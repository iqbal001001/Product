using System;

namespace Product.Domain
{
    public class ProductInfo : Data
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ProductType Type { get; set; }
        public bool Active { get; set; }
    }

    public enum ProductType
    {
        Books =1, Electronics=2, Food=3, Furniture=4, Toys=5
    }
}
