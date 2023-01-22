using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Business
{

    public class Products
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Image { get; set; }
        public string ImageURL { get; set; }

        public decimal Price { get; set; }

        public Boolean Available { get; set; }

        public DateTime Added_hour { get; set; }


        public Products() { }



    }
}
