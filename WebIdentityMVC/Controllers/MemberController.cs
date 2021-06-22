using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIdentityMVC.Controllers
{
    [Authorize(Roles = "Admin,Member")]

    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
