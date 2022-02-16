using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Catalog.Domain.Entities;
using Store.Catalog.Domain.Interfaces;
using Store.WebAPI.Service.Authorization;
using Store.WebAPI.Service.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.API.Controllers
{
    [Authorize]
    [Route("api/catalog")]
    public class CatalogController : BaseController
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("products")]
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        //[ClaimsAuthorize("catalog", "read")]
        [HttpGet]
        [Route("products/{id}")]
        public async Task<Product> GetProductById(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
