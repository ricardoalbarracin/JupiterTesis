using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.SEG;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.SEG.Controllers
{
    [Area("SEG")]
    public class UsuariosController : Controller
    {
        IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetListUsuarios([Kendo.Mvc.UI.DataSourceRequest] DataSourceRequest request)
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
        
    }
}