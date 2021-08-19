using Product.Web.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Domain;

namespace Product.Web.Extensions
{
    public static class ProductExtension
    {
        public static ProductDto ToDto(this ProductInfo product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = Enum.GetName(typeof(ProductType), product.Type),
                Active = product.Active
            };
        }

        public static ProductInsertDto ToInsertDto(this ProductInfo product)
        {
            return new ProductInsertDto
            {
                Name = product.Name,
                Price = product.Price,
                Type = Enum.GetName(typeof(ProductType), product.Type),
                Active = product.Active
            };
        }


    }


    public static class ProductDtoExtension
    {
        public static ProductInfo ToDomain(this ProductDto product, ProductInfo originalProduct = null)
        {
            if (originalProduct != null)
            {
                originalProduct.Name = product.Name;
                originalProduct.Price = product.Price;
                originalProduct.Type = (ProductType)Enum.Parse(typeof(ProductType), product.Type, true);
                originalProduct.Active = product.Active;
                return originalProduct;
            }

            return new ProductInfo
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Type = (ProductType)Enum.Parse(typeof(ProductType), product.Type, true),
                Active = product.Active
            };
        }

        public static ProductInfo ToDomain(this ProductInsertDto product, ProductInfo originalProduct = null)
        {
            if (originalProduct != null)
            {
                originalProduct.Name = product.Name;
                originalProduct.Price = product.Price;
                originalProduct.Type = (ProductType)Enum.Parse(typeof(ProductType), product.Type, true);
                originalProduct.Active = product.Active;

                return originalProduct;
            }

            return new ProductInfo
            {
                Name = product.Name,
                Price = product.Price,
                Type = (ProductType)Enum.Parse(typeof(ProductType), product.Type, true),
                Active = product.Active
            };
        }


    }
}
