using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DummyClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyResource.Controllers
{
    [Authorize]

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public IActionResult Manage()
        {
            return Redirect("http://localhost:5000/account/manage?returnUrl=http://localhost:50975/");
        }

        public IActionResult Register()
        {
            return Redirect("http://localhost:5000/account/register?returnUrl=http://localhost:50975/home/contact");
        }
        
        public IActionResult Login()
        {
            return Redirect(@"http://localhost:5000/account/login?returnUrl=http://localhost:50975/home/contact");
        }

        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            var accessToken = HttpContext.Authentication.GetTokenAsync("access_token").Result;

            await TempWebClient.LogOut(accessToken);                    

            return Redirect("http://localhost:50975/");
        }
    }
}
