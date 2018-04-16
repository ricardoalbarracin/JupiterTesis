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
    public class RolesController : Controller
    {
        IUsuarioDAOService _usuarioService;
        IRoleDAOService _roleService;
        IPermisoDAOService _permisoService;
        public RolesController(IUsuarioDAOService usuarioService, IRoleDAOService roleService, IPermisoDAOService permisoService)
        {
            _usuarioService = usuarioService;
            _roleService = roleService;
            _permisoService = permisoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetListRoles([DataSourceRequest] DataSourceRequest request)
        {
            var listRoles = _roleService.ListRoles();

            if (!listRoles.Success)
            {
                ModelState.AddModelError("Error", listRoles.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = listRoles.Data as List<Role>;
            return Json(usuarios.ToDataSourceResult(request));
        }
    }
}