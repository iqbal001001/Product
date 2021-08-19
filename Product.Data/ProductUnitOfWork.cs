using Product.RepositoryInterface;
using System.Threading.Tasks;

namespace Product.Data
{
    public class ProductUnitOfWork : IUnitOfWork
    {
        private ProductContext _context;

        protected ProductContext Context
        {
            get { return _context ?? (_context = ProductContextFactory.Get()); }
        }

        public void SaveChanges()
        {
            //Context.Commit();
            Context.SaveChanges();
        }
        public async Task SaveChangesAsync()
        {
            //Context.Commit();
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

