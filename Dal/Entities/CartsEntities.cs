using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Dal.Entities
{
    public class CartsEntities
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Product")]
        //public int ProductId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public string Status { get; set; }


        public List<CartsItemsEntities> CartItems { get; set; }

        //public virtual ProductsEntities Product { get; set; }
        [JsonIgnore]
        public virtual UsersEntities User { get; set; }


    }
}
