using Product.RepositoryInterface;

namespace Product.Data
{
        public class ProductContextFactory 
    {
            private static ProductContext _context;

            public static ProductContext Get()
            {
                if (_context == null) InitialiseContext();

                return _context;
            }

            public  static void InitialiseContext()
            {
                _context = new ProductContext();
            }
        }
}
