using System;

namespace Store.WebApp.MVC.Models.ViewModels.Product
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PathImage { get; set; }
        public int InventoryQuantity { get; set; }
    }
}
