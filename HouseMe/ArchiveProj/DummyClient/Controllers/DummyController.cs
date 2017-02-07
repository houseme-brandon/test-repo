using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DummyResource.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DummyController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            //throw new Exception("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
