using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class Products
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [IgnoreDataMember]
        public string ImageURL { get; set; }

        public decimal Price { get; set; }

        public Boolean Available { get; set; }

        public DateTime Added_hour { get; set; }


        public Products() { }



    }
}
