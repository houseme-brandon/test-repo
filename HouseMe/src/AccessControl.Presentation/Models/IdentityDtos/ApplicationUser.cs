using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AccessControl.Application.Models.IdentityDtos
{
    public class ApplicationUser: IdentityUser<Guid>
    {
    }
}
