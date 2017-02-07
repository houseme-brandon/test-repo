using System;
using System.Threading.Tasks;
using AccessControl.Domain;
using AccessControl.Domain.Services;
using AccessControl.Domain.Services.Interfaces;
using AccessControl.Web.Controllers.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.ValueObjects;

namespace AccessControl.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly AccountService accountService;
        private readonly IUserRepository userRepository;

        public AccountController(AccountService accountService, IUserRepository userRepository)
        {
            this.accountService = accountService;
            this.userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return BadRequest(model);
        
            var userId = Guid.NewGuid();
            var rsaId = RsaIdNumber.From(model.IdNumber);
            var mobileNum = TelephoneNumber.From(model.MobileNumber);
            var email = EmailAddress.From(model.Email);
            var firstName = HumanName.From(model.FirstName);
            var lastName = HumanName.From(model.LastName);


            var placeholderText = "<~>";
            var templateUrl = Url.Action("ConfirmEmail", "Account", new {userId = userId, code = placeholderText, returnUrl = returnUrl}, protocol: HttpContext.Request.Scheme);
            var urlBuilder = new CodedUrlBuilder(templateUrl, placeholderText);

            await accountService.RegisterAsync(userId, model.Password, rsaId, firstName, lastName, email, mobileNum, urlBuilder);
                        
            return Ok(userId);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel emailModel, string returnUrl = null)
        {
            //var confirmSuccessful = await accountService.ConfirmEmailAsnyc(emailModel.UserId, emailModel.Code);

            //if (confirmSuccessful)
            //{
            //    return Redirect(returnUrl);
            //}
            //else
            //{
            //    return BadRequest();
            //}            
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            // Require the user to have a confirmed email before they can log on.

            var idNumber = RsaIdNumber.From(model.IdNumber);

            var isRegistered = await userRepository.UserExistsAsync(idNumber);            
            if (!isRegistered)
            {
                return BadRequest();
            }

            var nonconfirmedEmailAddress = await userRepository.EmailConfirmed(idNumber);
            if (nonconfirmedEmailAddress)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }

            //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);                
            //if (result.Succeeded)
            //{
            //    _logger.LogInformation(1, "User logged in.");
            //    return RedirectToLocal(returnUrl);
            //}
            //if (result.RequiresTwoFactor)
            //{
            //    return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
            //}
            //if (result.IsLockedOut)
            //{
            //    _logger.LogWarning(2, "User account locked out.");
            //    return View("Lockout");
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //    return View(model);
            //}

            // If we got this far, something failed, redisplay form
        }
    }
}
