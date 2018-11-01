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
        IUserDAOService _usuarioService;
        IRoleDAOService _roleService;
        IPermissionDAOService _permisoService;
        public RolesController(IUserDAOService usuarioService, IRoleDAOService roleService, IPermissionDAOService permisoService)
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
            var listRoles = _roleService.GetListRoles();

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