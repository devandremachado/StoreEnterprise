using Microsoft.Extensions.Options;
using Store.WebApp.MVC.Extensions;
using Store.WebApp.MVC.Helpers;
using Store.WebApp.MVC.Models.ViewModels.Product;
using Store.WebApp.MVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient,
                          IOptions<AppSettings> appSettings)
        {

            httpClient.BaseAddress = new Uri(appSettings.Value.API_CatalogUrl);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products");

            HandleResponseErrors(response);

            return await DeserializeResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid Id)
        {
            var response = await _httpClient.GetAsync($"/api/catalog/products/{Id}");

            HandleResponseErrors(response);

            return await DeserializeResponse<ProductViewModel>(response);
        }
    }
}
