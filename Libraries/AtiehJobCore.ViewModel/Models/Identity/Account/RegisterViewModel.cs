using AtiehJobCore.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AtiehJobCore.ViewModel.Models.Identity.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "(*)")]
        [Display(Name = "User name")]
        //[RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "لطفا تنها از اعداد و حروف انگلیسی استفاده نمائید")]
        [Remote("ValidateUsername", "Account",
            AdditionalFields = nameof(Email) + "," + Properties.AntiForgeryToken, HttpMethod = "POST")]
        [RegularExpression("^[a-zA-Z_]*$", ErrorMessage = "Please use only English letters")]
        public string Username { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "(*)")]
        [StringLength(450)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
                          ErrorMessage = "Please use only Persian letters")]
        public string FirstName { get; set; }

        [Display(Name = "Family")]
        [Required(ErrorMessage = "(*)")]
        [StringLength(450)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF\s]*$",
                          ErrorMessage = "Please use only Persian letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "(*)")]
        [Remote("ValidateUsername", "Account",
            AdditionalFields = nameof(Username) + "," + Properties.AntiForgeryToken, HttpMethod = "POST")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "(*)")]
        [StringLength(100, ErrorMessage = "{0} must be at least {2} and maximum {1} characters.", MinimumLength = 6)]
        [Remote("ValidatePasswordByUserName", "Account",
            AdditionalFields = nameof(Username) + "," + Properties.AntiForgeryToken, HttpMethod = "POST")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "(*)")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(nameof(Password), ErrorMessage = "")]
        public string ConfirmPassword { get; set; }
    }
}