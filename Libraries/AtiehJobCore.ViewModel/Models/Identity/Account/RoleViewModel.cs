using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AtiehJobCore.ViewModel.Models.Identity.Account
{
    public class RoleViewModel
    {
        [HiddenInput]
        public string Id { set; get; }

        [Required(ErrorMessage = "(*)")]
        [Display(Name = "نام نقش")]
        public string Name { set; get; }
    }
}
