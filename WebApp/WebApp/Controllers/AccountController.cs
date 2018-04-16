using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{


    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        ISeguridadService _seguridadService;
        IHostingEnvironment _environment;
        public AccountController(ISeguridadService seguridadService, IHostingEnvironment environment)
        {
            _seguridadService = seguridadService;
            _environment = environment;
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

        public ActionResult UserImage()
        {
            var path = Path.Combine(_environment.WebRootPath, "img\\avatars\\Women.png");
            var binImage =  System.IO.File.ReadAllBytes(path);
            return base.File(binImage, "image/jpeg");
        }

    }
}