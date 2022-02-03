using Store.WebApp.MVC.Models.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.WebApp.MVC.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid Id);

    }
}
