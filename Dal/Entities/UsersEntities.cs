using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entities
{
    public enum Role
    {
        User = 0,
        Seller = 1,
        Admin = 2,
    }


    public class UsersEntities
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Pseudo { get; set; }

        [DataType(DataType.Password)]
        [JsonIgnore]
        public string Password { get; set; }


        public Role role { get; set; }
        

        

    }
}
