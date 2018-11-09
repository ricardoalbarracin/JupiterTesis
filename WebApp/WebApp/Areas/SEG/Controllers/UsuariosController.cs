using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Fliters;
using WebApp.Helpers;

namespace WebApp.Areas.SEG.Controllers
{
    [Area("SEG")]
    [HasPermission("ADMIN")]
    public class UsuariosController : Controller
    {
        IUserDAOService _usuarioService;
        ISecurityService _seguridadService;

        public UsuariosController(IUserDAOService usuarioService,ISecurityService seguridadService)
        {
            _usuarioService = usuarioService;
            _seguridadService = seguridadService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetListUsuarios([DataSourceRequest] DataSourceRequest request)
        {
            var getListUsuarios = _usuarioService.GetListUser();

            if (!getListUsuarios.Success)
            {
                ModelState.AddModelError("Error", getListUsuarios.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = getListUsuarios.Data as List<UserIdentity>;
            return Json(usuarios.ToDataSourceResult(request));
        }

        public IActionResult ActualizarUsuario(int id)
        {
            var result = _usuarioService.GetUserEditById(id);
            ViewBag.Container =ControllerContext.RouteData.Values["action"].ToString();
            return PartialView(result.Data);
        }

        public IActionResult CrearUsuario()
        {
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            return PartialView();
        }

        public JsonResult ValidarActualizarUsuario(User usuario)
        {
            var result = _seguridadService.ValidateUserUpdate(usuario);
            return new JsonResult(result);
        }


        [HttpPost]
        public JsonResult Finish(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _seguridadService.UpdUserRolesPermissions(secctions);
            return new JsonResult(result);
        }
        
        public IActionResult AgregarUsuarioRole(string operacion)
        {
            ViewBag.Operacion = operacion;
            return PartialView();
        }

        public IActionResult AgregarUsuarioPermiso(string operacion)
        {
            ViewBag.Operacion = operacion;
            return PartialView();
        }

        [HttpGet]
        public ActionResult GetListEstados([DataSourceRequest]DataSourceRequest request)
        {
            var dictionary = new Dictionary<bool, string>
            {
                { true, "Activo" },
                { false, "Inactivo" }
            };

            var list = new SelectList(dictionary, "Key", "Value");

            return Json(list);
        }

        [HttpPost]
        public JsonResult ResetPassword(User usuario)
        {
            var result = new Result<string>();
            if (!ModelState.IsValid)
            {
                result.Message = "Modelo invalido";
                return Json(result);
            }
            result = _seguridadService.ResetPassword(usuario);
            return Json(result);
        }

        public JsonResult ValidarCrearUsuario(User usuario)
        {
            var result = _seguridadService.ValidateUserCreate(usuario);
            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult FinishCrearUsuario(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _seguridadService.InsUserRolesPermissions(secctions);
            return new JsonResult(result);
        }

    }
}