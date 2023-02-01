using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Dal
{
    /// <summary>
    /// Modèle validation user Input
    /// </summary>
    public class UserInput
    {
        [Required]
        [MaxLength(100, ErrorMessage = "The maximum size of the nickname has been reached !")]
        public string Pseudo { get; set; }


        [EmailAddress]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The password must contain at least 8 characters including at least one lowercase, at least one uppercase, at least one number and at least one special character
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{8,}$", ErrorMessage = "The password must contain at least 8 characters including at least one lowercase, at least one uppercase, at least one number and at least one special character")]
        public string Password { get; set; }

        /// <summary>
        /// 0 = User
        /// 1 = Seller
        /// 2 = Admin
        /// </summary>
        [Required]
        public Role role { get; set; }

    }

    /// <summary>
    /// User Register Validation Template
    /// </summary>
    public class UserInputRegister
    {

        [Required]
        [MaxLength(100)]
        public string Pseudo { get; set; }

        /// <summary>
        /// Valid email address
        /// </summary>
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [EmailAddress]
        [Required]
        [Compare("Email", ErrorMessage = "Email addresses do not match.")]
        [Display(Name = "email confirmation")]
        public string ConfirmEmail { get; set; }

        /// <summary>
        /// The password must contain at least 8 characters including at least one lowercase, at least one uppercase, at least one number and at least one special character
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*])[A-Za-z\\d!@#$%^&*]{8,}$", ErrorMessage = "The password must contain at least 8 characters including at least one lowercase, at least one uppercase, at least one number and at least one special character")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "passwords do not match.")]
        public string ConfirmPassword { get; set; }


        /// <summary>
        /// 0 = User
        /// 1 = Seller
        /// 2 = Admin
        /// </summary>
        [Required]
        public Role role { get; set; }
    }


    /// <summary>
    /// Model validation for user Login
    /// </summary>
    public class UserInputLogin
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }


}
