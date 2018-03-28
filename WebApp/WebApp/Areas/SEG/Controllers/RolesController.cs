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
        IUsuarioService _usuarioService;
        IRoleService _roleService;
        IPermisoService _permisoService;
        public RolesController(IUsuarioService usuarioService, IRoleService roleService, IPermisoService permisoService)
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