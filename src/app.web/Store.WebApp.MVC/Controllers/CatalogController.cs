using Microsoft.AspNetCore.Mvc;
using Store.WebApp.MVC.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Controllers
{
    public class CatalogController : BaseController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        [HttpGet]
        [Route("")]
        [Route("vitrine")]
        public async Task<IActionResult> Index()
        {
            var products = await _catalogService.GetAll();

            return View(products);
        }

        [HttpGet]
        [Route("produto-detalhe/{id}")]
        public async Task<IActionResult> ProductDetails(Guid Id)
        {
            var product = await _catalogService.GetById(Id);

            return View(product);
        }
    }
}
