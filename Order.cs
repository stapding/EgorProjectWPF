using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rus
{
    public class Order
    {

        public User user
        {
            get; set;
        }
        public List<Product> products { get; set; }

        public DateTime date { get; set; }

        public string id { get; set; }


        public Order(string id,DateTime date,User user,List<Product> products)
        {
            this.id = id;
            this.date = date;
            this.user = user;
            this.products = products;
        }



    }
}
