using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public enum Role { 
        User,
        Seller,
        Admin,
    }

    
    public class Users
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Pseudo { get; set; }

        public string Password { get; set; }

        public Role role { get; set; }

    }
}
