using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccessControl.Web.Controllers.Account
{
    public class ConfirmEmailModel
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Code { get; set; }       
    }
}
