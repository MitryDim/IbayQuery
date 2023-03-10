using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public class ProductsEntities
    {
        [Key]
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(200)]
        public string Name { get; set; }

        [NotMapped]
        [JsonIgnore]
        public IFormFile Image { get; set; }

        public string ImageURL { get; set; }

        public decimal Price { get; set; }

        public Boolean Available { get; set; }

        public DateTime Added_Hour { get; set; }


        public int OwnedId { get; set; }

       



    }
}
