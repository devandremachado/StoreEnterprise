using Microsoft.Extensions.DependencyInjection;
using Store.Catalog.Domain.Interfaces;
using Store.Catalog.Infra.Data.Context;
using Store.Catalog.Infra.Data.Repositories;

namespace Store.Catalog.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
