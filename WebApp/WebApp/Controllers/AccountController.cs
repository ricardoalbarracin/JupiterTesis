using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Models;
using WebApp.Models.AccountViewModels;
using WebApp.Services;

namespace WebApp.Controllers
{
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {


        IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync();

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Login(Usuario usuario)
        {
            var result = _accountService.Login(usuario);
            var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Ricardo")
                    };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.AuthenticateAsync("JupiterCookieAuthenticationScheme");
            return Json(result);
        }








    }
}
