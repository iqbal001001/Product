using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Web.Dto
{
    public class ProductDto : ProductInsertDto
    {
        public int  Id { get; set; }
    }

    public class ProductInsertDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }

    }


}
