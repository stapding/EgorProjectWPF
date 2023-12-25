using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace Rus
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public Category Category { get; set; }

        public int Count { get; set; }

        public string Url { get; set; }


        public Product(string name, string description,Category category, int price, int count, string url)
        {
            this.Name = name;
            this.Description = description;
            this.Category = category;
            this.Price = price;
            this.Count = count;
            this.Url = url;  
           
        }

        public Product()
        {
            
        }


    }
}
