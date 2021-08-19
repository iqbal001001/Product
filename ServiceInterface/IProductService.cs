using System;
using System.Collections.Generic;
using Product.Domain;
using System.Threading.Tasks;

namespace Product.ServiceInterface
{
    public interface IProductService
    {
        Task<bool> AnyByIdAsync(int id);
        Task<int> CountAsync();
      Task<List<ProductInfo>> GetAllAsync(string sort,
            int page, int pageSize);
        Task<ProductInfo> GetByIdAsync(int id);
        Task AddAsync(ProductInfo product);
        Task UpdateAsync(ProductInfo product);
        Task DeleteAsync(int id);
        bool Validate(ProductInfo product);
    }
}
