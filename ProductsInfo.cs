using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rus
{
    public class ProductsInfo
    {

        public List<Product> products { get; set; }
        public List<Category> category { get; set; }

        
        public ProductsInfo(List<Product> products, List<Category> category) {
            this.products = products;
            this.category = category;
        }

    }
}
