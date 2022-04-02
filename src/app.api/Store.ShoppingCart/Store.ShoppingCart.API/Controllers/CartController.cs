using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Cart.Domain.Entities;
using Store.Cart.Infra.Data.Context;
using Store.WebAPI.Service.Controllers;
using Store.WebAPI.Service.User.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Cart.API.Controllers
{
    [Authorize]
    [Route("api/cart")]
    public class CartController : BaseController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet]
        public async Task<CartCustomer> GetCart()
        {
            return await GetCartCustomer() ?? new CartCustomer();
        }

        [HttpPost]
        public async Task<IActionResult> AddItemCart([FromBody] CartItem item)
        {
            var cart = await GetCartCustomer();

            if (cart is null)
                HandleNewCart(item);
            else
                HandleExistingCart(cart, item);

            ValidateCart(cart);
            if (IsValid() == false)
                return CustomResponse();

            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                AddError("Can't add item to shopping cart");

            return CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem([FromQuery] Guid productId, [FromBody] CartItem item)
        {
            var cart = await GetCart();
            var cartItem = await GetValidateCartItem(productId, cart, item);

            if (cartItem is null)
                return CustomResponse();

            cart.UpdateItemQuantity(cartItem, item.Quantity);

            ValidateCart(cart);
            if (IsValid() == false)
                return CustomResponse();

            _context.CartItem.Update(cartItem);
            _context.CartCustomer.Update(cart);

            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                AddError("Couldn't  update item to shopping cart");

            return CustomResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem([FromQuery] Guid productId)
        {
            var cart = await GetCartCustomer();
            var cartItem = await GetValidateCartItem(productId, cart);

            if (cartItem is null)
                return CustomResponse();

            ValidateCart(cart);
            if (IsValid() == false)
                return CustomResponse();

            cart.RemoveItem(cartItem);

            _context.CartItem.Remove(cartItem);
            _context.CartCustomer.Update(cart);

            var result = await _context.SaveChangesAsync();
            if (result <= 0)
                AddError("Couldn't delete item to shopping cart");

            return CustomResponse();
        }


        #region Private Methods
        private async Task<CartCustomer> GetCartCustomer()
        {
            return await _context.CartCustomer
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.CustomerId == _user.GetId());
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CartCustomer(_user.GetId());
            cart.AddItem(item);

            _context.CartCustomer.Add(cart);
        }

        private void HandleExistingCart(CartCustomer cart, CartItem item)
        {
            var productExists = cart.ProductAlreadyExistsInCart(item);
            cart.AddItem(item);

            if (productExists)
            {
                _context.CartItem.Update(cart.GetProductById(item.ProductId));
            }
            else
            {
                _context.CartItem.Add(item);
            }

            _context.CartCustomer.Update(cart);
        }

        private async Task<CartItem> GetValidateCartItem(Guid productId, CartCustomer cart, CartItem item = null)
        {
            if (item is not null && productId != item.ProductId)
            {
                AddError("Id Item is invalid");
                return null;
            }

            if (cart is null)
            {
                AddError("Cart Not Found");
                return null;
            }

            var itemCart = await _context.CartItem.FirstOrDefaultAsync(x => x.CartId == cart.Id && x.ProductId == productId);

            if (itemCart is null || cart.ProductAlreadyExistsInCart(item) == false)
            {
                AddError("Item not found on Cart");
                return null;
            }

            return itemCart;
        }

        private bool ValidateCart(CartCustomer cart)
        {
            if (cart.IsValid())
                return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddError(e.ErrorMessage));
            return false;
        }
        #endregion
    }
}
