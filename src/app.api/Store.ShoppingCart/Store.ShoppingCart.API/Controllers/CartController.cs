using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Cart.Domain.Entities;
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

        public CartController(IAspNetUser user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<CartCustomer> GetCart()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateItemCart(CartItem item)
        {
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
    }
}
