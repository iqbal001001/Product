using Product.Domain;
using Product.RepositoryInterface;

namespace Product.Data.Repository
{
    public class ProductRepository : RepositoryBase<ProductInfo>, IProductRepository
    {
        public ProductRepository() : base() { }
    }
}
