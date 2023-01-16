using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Price { get; set; }

        public Boolean Available { get; set; }

        public DateTime Added_hour { get; set; }


        public Product() { }



    }
}
