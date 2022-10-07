using Gomar.Models;
using Gomar.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gomar.Controllers
{
    public class AdminController : Controller
    {
        UserManager _userManager;

        public AdminController(UserManager userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Main()
        {
            return View();
        }

        [Route("Admin")]
        public IActionResult LogIn()
        {
            return View();
        }

        [Route("Admin")]
        [HttpPost]
        public async Task<IActionResult> LogInAsync(LogInViewModel form)
        {
            if (!ModelState.IsValid)
                return View(form);
            try
            {
                await _userManager.SignIn(this.HttpContext, form.Email, form.Password);
                return RedirectToAction("Main");
            }
            catch (Exception ex)
            {
                ViewData["ValidationSummary"] = ex.Message;
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
