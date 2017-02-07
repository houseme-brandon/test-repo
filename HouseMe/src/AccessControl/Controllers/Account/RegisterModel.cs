using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Web.Controllers.Account
{
    public class RegisterModel
    {
        [Required]
        public string IdNumber { get; set; }

        [Required]
        //Add first name validation
        public string FirstName { get; set; }
       
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string MobileNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
