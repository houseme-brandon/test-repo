using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AccessControl.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
