using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Cart.Domain.Entities;
using Store.Cart.Infra.Data.Context;
using Store.WebAPI.Service.Controllers;
using Store.WebAPI.Service.User.Interfaces;
using System;
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
        public async Task<IActionResult> CreateItemInCart(CartItem item)
        {
            var cart = await GetCartCustomer();

            if (cart is null)
                HandleNewCart(item);

            else
                HandleExistingCart(cart, item);

            if (IsValid() == false)
                return CustomResponse();

            var result = await _context.SaveChangesAsync();
            if (result <= 0) 
                AddError("Can't add item to shopping cart");

            return CustomResponse();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCartItem(Guid IdItem, CartItem item)
        {
            return CustomResponse();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem(Guid IdItem)
        {
            return CustomResponse();
        }

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
    }
}
