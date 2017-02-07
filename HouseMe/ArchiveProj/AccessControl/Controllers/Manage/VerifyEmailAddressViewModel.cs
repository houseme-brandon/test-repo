using System.ComponentModel.DataAnnotations;

namespace AccessControl.Controllers.Manage
{
    public class VerifyEmailAddressViewModel
    {
        [Required]
        public string Code { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public string ReturnUrl { get; set; }
    }
}
