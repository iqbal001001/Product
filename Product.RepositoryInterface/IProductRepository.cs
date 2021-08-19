using System;
using System.Collections.Generic;
using System.Text;
using Product.Domain;

namespace Product.RepositoryInterface
{
    public interface IProductRepository: IRepositoryBase<ProductInfo>
    {
    }
}
