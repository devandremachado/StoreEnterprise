using Microsoft.AspNetCore.Mvc;
using Store.Catalog.Domain.Entities;
using Store.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    public class CatalogController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("products")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productRepository.GetAll();
        }

        [HttpGet]
        [Route("products/{id}")]
        public async Task<Product> GetProductById(Guid id)
        {
            return await _productRepository.GetById(id);
        }
    }
}
