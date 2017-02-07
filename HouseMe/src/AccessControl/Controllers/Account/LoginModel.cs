using System.ComponentModel.DataAnnotations;

namespace AccessControl.Web.Controllers.Account
{
    public class LoginModel
    {
        [Required]
        public string IdNumber{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
