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
        /// <summary>
        /// Name of product
        /// </summary>
        [Required]
        [MinLength(3)]
        [MaxLength(200)]
        public string Name { get; set; }


        [Required]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Product price example : 10.33
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// True : Makes the product active
        /// False : Makes the product inactive
        /// </summary>
        [Required]
        public Boolean Available { get; set; }


    }


}
