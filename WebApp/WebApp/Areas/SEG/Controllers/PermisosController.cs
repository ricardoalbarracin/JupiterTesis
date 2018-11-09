using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.SEG;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Fliters;

namespace WebApp.Areas.SEG.Controllers
{
    [Area("SEG")]
    [HasPermission("ADMIN")]
    public class PermisosController : Controller
    {
        IPermissionDAOService _permisoService;

        public PermisosController (IUserDAOService usuarioService, IPermissionDAOService permisoService )
        {
            _permisoService = permisoService;
           
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetListPermisos([DataSourceRequest] DataSourceRequest request)
        {
            var listPermisos = _permisoService.GetListPermissions();

            if (!listPermisos.Success)
            {
                ModelState.AddModelError("Error", listPermisos.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = listPermisos.Data as List<Permission>;
            return Json(usuarios.ToDataSourceResult(request));
        }
    }
}