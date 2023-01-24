using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    /// <summary>
    /// Modèle validation user Input
    /// </summary>
    public class UserInput
    {
        [Required]
        public string Pseudo { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        public Role role { get; set; }

    }

    /// <summary>
    /// Modèle validation User Register
    /// </summary>
    public class UserInputRegister
    {

        [Required]
        public string Pseudo { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [EmailAddress]
        [Required]
        [Compare("Email", ErrorMessage = "The email addresses do not match.")]
        [Display(Name = "Confirmation Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }


        [Required]
        public Role role { get; set; }
    }


    /// <summary>
    /// Modèle validation for user Login
    /// </summary>
    public class UserInputLogin
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

    }


}
