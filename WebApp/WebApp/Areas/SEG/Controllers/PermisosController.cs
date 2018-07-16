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
    public class PermisosController : Controller
    {
        IPermisoDAOService _permisoService;

        public PermisosController (IUsuarioDAOService usuarioService, IPermisoDAOService permisoService )
        {
            _permisoService = permisoService;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetListPermisos([DataSourceRequest] DataSourceRequest request)
        {
            var listPermisos = _permisoService.GetListPermisos();

            if (!listPermisos.Success)
            {
                ModelState.AddModelError("Error", listPermisos.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = listPermisos.Data as List<Permiso>;
            return Json(usuarios.ToDataSourceResult(request));
        }
    }
}