using Store.Shared.DomainObjects;
using System;

namespace Store.Catalog.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool Active { get; private set; }
        public decimal Price { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string PathImage { get; private set; }
        public int InventoryQuantity { get; private set; }
    }
}
