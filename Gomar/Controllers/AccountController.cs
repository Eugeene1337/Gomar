using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gomar.Controllers
{
    public class AccountController : Controller
    {
        UserManager _userManager;

        public AccountController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [Route("Admin")]
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult LogIn(LogInViewModel form)
        {
            if (!ModelState.IsValid)
                return View(form);
            try
            {
                _userManager.SignIn(this.HttpContext, form.Email, form.Password);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("summary", ex.Message);
                return View(form);
            }
        }

        [Route("Logout")]
        public IActionResult Logout()
        {
            _userManager.SignOut(this.HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
