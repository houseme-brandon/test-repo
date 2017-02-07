using AccessControl.Domain.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using AccessControl.Domain;
using AccessControl.Domain.UserAggregate;
using AccessControl.Infrastructure.Identity;
using AccessControl.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Shared.Domain.ValueObjects;

namespace AccessControl.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        //private readonly IEmailSender emailSender;
        //private readonly ISmsSender smsSender;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        private async Task<ApplicationUser> GetUser(Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id), $"Given {id} - no user with id was found.");
            }

            return user;
        }
        
        public async Task<bool> UserExistsAsync(RsaIdNumber idNumber)
        {
            return await userManager.FindByIdAsync(idNumber.ToString()) != null;            
        }

        public async Task CreateUserAsync(User user, string password)
        {
            var appUser = user.ToDto();
            var result = await userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => $"{x.Code} - {x.Description}").Aggregate((x, y) => x + ", " + y);
                throw new InvalidOperationException($"Create user failed with errors - {errors}");
            }
        }

        public async Task<bool> IdNumberExistsAsync(RsaIdNumber idNumber)
        {
            return await userManager.FindByNameAsync(idNumber.IdentityNumber) != null;
        }

        public async Task<string> GenerateConfirmationUrlAsync(User user, CodedUrlBuilder builder)
        {
            var appUser = user.ToDto();
            var code = await userManager.GenerateEmailConfirmationTokenAsync(appUser);

            var callbackUrl = builder.BuiltUrl(code);
            var shortenedCallback = GoogleUrlShortener.ShortenUrl(callbackUrl);

            return shortenedCallback;
        }

        public async Task<bool> ConfirmEmailAddress(Guid id, string code)
        {
            var user = await GetUser(id);
            var result = await userManager.ConfirmEmailAsync(user, code);
            
            return result.Succeeded;
        }

        public async Task SignInUser(Guid id)
        {
            var user = await GetUser(id);
            await signInManager.SignInAsync(user, isPersistent: false);
        }
        
        public Task<bool> EmailConfirmed(RsaIdNumber id)
        {
            //var user = await GetUser(id);

            // await userManager.IsEmailConfirmedAsync()

            throw new NotImplementedException();
        }

        public Task PasswordLogin(RsaIdNumber id, string password)
        {
            throw new NotImplementedException();
            //await signInManager.CheckPasswordSignInAsync()
        }

        public Task<bool> UserExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
