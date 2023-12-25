using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rus
{
    public class Category
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public Category( string name)
        {
            this.Name = name;
        }

        public Category(int id)
        {
            this.Id = id;
        }

        public Category() { }

    }
}
