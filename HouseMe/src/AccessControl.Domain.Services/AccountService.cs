using System;
using System.Threading.Tasks;
using AccessControl.Domain.Services.Enums;
using AccessControl.Domain.Services.Interfaces;
using AccessControl.Domain.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.ValueObjects;

namespace AccessControl.Domain.Services
{
    public class AccountService
    {
        private readonly IUserRepository userRepository;
        private readonly INotificationDispatcher dispatcher;

        public AccountService(IUserRepository userRepository, INotificationDispatcher dispatcher)
        {
            this.userRepository = userRepository;
            this.dispatcher = dispatcher;
        }

        public async Task RegisterAsync(Guid id, string password, RsaIdNumber idNumber, HumanName firstName, HumanName lastName,
            EmailAddress emailAddress, TelephoneNumber mobileNumber, CodedUrlBuilder urlBuilder)
        {
            var userExists = await userRepository.UserExistsAsync(id);
            if (userExists)
            {
                throw new InvalidOperationException($"Given: {id} - User with that id has already been registered.");
            }

            var userRegistered = userRepository.IdNumberExistsAsync(idNumber);
            if (await userRegistered)
            {
                throw new ArgumentOutOfRangeException(nameof(idNumber),
                    $"Given: {idNumber} - User with that id number has already been registered.");
            }

            var newUser = User.From(id, idNumber, firstName, lastName, emailAddress, mobileNumber);

            await userRepository.CreateUserAsync(newUser, password);
            await SendConfirmationEmailAsync(newUser, urlBuilder);
        }

        public async Task SendConfirmationEmailAsync(User user, CodedUrlBuilder urlBuilder)
        {
            var confirmationUrl = await userRepository.GenerateConfirmationUrlAsync(user, urlBuilder);

            dispatcher.Dispatch(NotificationDispatchType.ConfirmationEmail, user, confirmationUrl);
        }

        public async Task<bool> ConfirmEmailAsync (Guid id, string code)
        {
            var noUser = await userRepository.UserExistsAsync(id);

            if (noUser)
            {
                throw new InvalidOperationException($"Given: {id} - No user exists with that id.");
            }
            
            var confirmSuccessful = await userRepository.ConfirmEmailAddress(id, code);

            if (confirmSuccessful)
            {
                await userRepository.SignInUser(id);
            }

            return confirmSuccessful;
        }

        public async Task<bool> LoginAccount (RsaIdNumber idNumber, string password)
        {
            return true;
            // var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            // if (result.Succeeded)
            // {
            //     _logger.LogInformation(1, "User logged in.");
            //     return RedirectToLocal(returnUrl);
            // }
            // if (result.RequiresTwoFactor)
            // {
            //     return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            // }
            // if (result.IsLockedOut)
            // {
            //     _logger.LogWarning(2, "User account locked out.");
            //     return View("Lockout");
            // }
            //return confirmSuccessful;
        }

    }
}
