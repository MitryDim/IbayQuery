using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{

    public class UserInput
    {
        [Required]
        public string Pseudo { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; }

        [Required]
        public Role role { get; set; }

    }

    public class UserInputLogin
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; }

    }


}
