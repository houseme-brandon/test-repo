using System.ComponentModel.DataAnnotations;

namespace AccessControl.Controllers.Manage
{
    public class ChangePhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
