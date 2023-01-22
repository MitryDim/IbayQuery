using AutoMapper.Configuration.Annotations;
using Dal.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BLL.Models
{
     public class UsersModel
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }


        public string Pseudo { get; set; }


        public string Password { get; set; }


        public Role role { get; set; }


    }
}
