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
using WebApp.Helpers;

namespace WebApp.Areas.SEG.Controllers
{
    [Area("SEG")]
    public class UsuariosController : Controller
    {
        IUsuarioDAOService _usuarioService;
        ISeguridadService _seguridadService;

        public UsuariosController(IUsuarioDAOService usuarioService,ISeguridadService seguridadService)
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
            var getListUsuarios = _usuarioService.GetListUsuarios();

            if (!getListUsuarios.Success)
            {
                ModelState.AddModelError("Error", getListUsuarios.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = getListUsuarios.Data as List<UsuarioIdentity>;
            return Json(usuarios.ToDataSourceResult(request));
        }

        public IActionResult ActualizarUsuario(int id)
        {
            var result = _usuarioService.GetUsuarioEditById(id);
            ViewBag.Container =ControllerContext.RouteData.Values["action"].ToString();
            return PartialView(result.Data);
        }

        public IActionResult CrearUsuario()
        {
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            return View();
        }

        public JsonResult ValidarActualizarUsuario(Usuario usuario)
        {
            var result = _seguridadService.ValidarActualizarUsuario(usuario);
            return new JsonResult(result);
        }


        [HttpPost]
        public JsonResult Finish(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _seguridadService.UpdUsuarioRolesPermisos(secctions);
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
        public JsonResult ResetPassword(Usuario usuario)
        {
            var result = new Result();
            if (!ModelState.IsValid)
            {
                result.Message = "Modelo invalido";
                return Json(result);
            }
            result = _seguridadService.ResetPassword(usuario);
            return Json(result);
        }

        public JsonResult ValidarCrearUsuario(Usuario usuario)
        {
            var result = _seguridadService.ValidarCrearUsuario(usuario);
            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult FinishCrearUsuario(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _seguridadService.InsUsuarioRolesPermisos(secctions);
            return new JsonResult(result);
        }

    }
}