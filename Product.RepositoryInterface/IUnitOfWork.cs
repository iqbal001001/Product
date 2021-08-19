using System.Threading.Tasks;

namespace Product.RepositoryInterface
{
    public interface IUnitOfWork
    {
        public void SaveChanges();
        public void Dispose();

         Task SaveChangesAsync();
    }
}
