using System;
using Product.ServiceInterface;
using Product.RepositoryInterface;
using Product.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Produt.Service
{
    public class ProductService : IProductService
    {
        private  IProductRepository _productRepo { get; set; }
        private  IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountAsync()
        {
            return await _productRepo.CountAsync();
        }
        public async Task<List<ProductInfo>> GetAllAsync(
            string sort = "Id",
            int page = 1, int pageSize = 5)
        {
          return  await _productRepo.GetAsync(sort, (page-1)* pageSize, pageSize);
        }

        public async Task<ProductInfo> GetByIdAsync(int id)
        {
            return await _productRepo.GetByIdAsync(id);
        }
        public async Task<bool> AnyByIdAsync(int id)
        {
            return await _productRepo.AnyAsync(p => p.Id == id);
        }

        public async Task AddAsync(ProductInfo product)

        {
             await _productRepo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductInfo product)
        {
             _productRepo.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }

        public bool Validate(ProductInfo product)
        {
            if (string.IsNullOrEmpty(product.Name) )
                return false;
            else if (product.Price <= 0)
                return false;

            return true;
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
