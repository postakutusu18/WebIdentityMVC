﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebIdentityMVC.Context;
using WebIdentityMVC.Models;

namespace WebIdentityMVC.Controllers
{
    [AutoValidateAntiforgeryToken]

    public class HomeController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GirisYap(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityResult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);

                if (identityResult.IsLockedOut)
                {
                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));

                    var kisitlananSure = gelen.Value;

                    var kalanDakika = kisitlananSure.Minute - DateTime.Now.Minute;

                    ModelState.AddModelError("", $"3 kere yanlış girdiğiniz için hesabınız {kalanDakika} dk kilitlenmiştir ");
                    return View("Index", model);
                }

                if (identityResult.IsNotAllowed)
                {

                    ModelState.AddModelError("", "Email adresinizi lütfen doğrulayınız.");
                    return View("Index", model);
                }

                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }
                var yanlisGirilmeSayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));
                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı {3 - yanlisGirilmeSayisi} kadar yanlış girerseniz hesabınız bloklanacak");
            }
            return View("Index", model);
        }
        public IActionResult KayitOl()
        {
            return View(new UserSignUpViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(UserSignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
