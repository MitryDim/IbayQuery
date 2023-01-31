using Dal.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace IbayQuery
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
            public string Email { get; set; }
            public string Pseudo { get; set; }
            public string Password { get; set; }
            public Role role { get; set; }


        }

}
