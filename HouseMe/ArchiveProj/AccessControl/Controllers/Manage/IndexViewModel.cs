using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AccessControl.Controllers.Manage
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public bool PhoneNumberIsVerified { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }
    }
}
