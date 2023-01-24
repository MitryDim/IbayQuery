using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class ProductsInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Boolean Available { get; set; }


    }


}
