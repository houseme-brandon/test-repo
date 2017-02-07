using AccessControl.Domain.UserAggregate;
using AccessControl.Infrastructure.Identity;

namespace AccessControl.Infrastructure
{
    public static class Mapper
    {
        public static ApplicationUser ToDto(this User user)
        {
            return new ApplicationUser()
            {
                Id = user.Id,
                UserName = user.IdNumber.ToString(),
                Email = user.EmailAddress.ToString(),
                PhoneNumber = user.MobileNumber.ToString(),                
                FirstName = user.FirstName.ToString(),
                LastName = user.LastName.ToString()
            };
        }
    }
}
