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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.Models;
using WebApp.Models.AccountViewModels;
using WebApp.Utils;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    

    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        ISeguridadService _seguridadService;
        public AccountController(ISeguridadService seguridadService)
        {
            _seguridadService = seguridadService;
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
            var result = new Result();
            if (!ModelState.IsValid)
            {
                result.Message = "Modelo invalido";
                return Json(result);
            }
            //result = _seguridadService.Login(usuario);
            //if (!result.Success)
            //    return Json(result);
            var usuarioDB = result.Data as UsuarioIdentity;
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Name, $"{usuarioDB.PrimerNombre} {usuarioDB.SegundoNombre} {usuarioDB.PrimerApellido} {usuarioDB.SegundoApellido}")
                new Claim(ClaimTypes.Name, $"Ricardo")
            };

            var userIdentity = new ClaimsIdentity(claims, "login");

            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
            await HttpContext.SignInAsync(principal);
            //HttpContext.Session.SetObject("Usuario", usuarioDB);
            //var a = HttpContext.Session.GetUser();
            result.Success = true;
            return Json(result);
        }

        

    }
}