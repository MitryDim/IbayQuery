using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public enum Role
    {
        User = 0,
        Seller = 1,
        Admin = 2,
    }


    public class Users
    {
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Pseudo { get; set; }


        public string Password { get; set; }


        public Role role { get; set; }

    }
}
