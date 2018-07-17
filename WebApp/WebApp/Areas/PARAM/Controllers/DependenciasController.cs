using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("PARAM")]
    public class DependenciasController : Controller
    {
        IDependenciaDAOService _DependenciaService;
        public DependenciasController(IDependenciaDAOService DependenciaService)
        {
            _DependenciaService = DependenciaService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListDependencias([DataSourceRequest] DataSourceRequest request, int? id)
        {
            var getListDependencias = _DependenciaService.GetListDependencias();

            if (!getListDependencias.Success)
            {
                ModelState.AddModelError("Error", getListDependencias.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }

            var dependencias = from e in getListDependencias.Data
                               where (id.HasValue ? e.padre_id == id : e.padre_id == null)
                               select new
                               {
                                   Codigo = e.Codigo,
                                   id=e.Id,
                                   hasChildren = getListDependencias.Data.Where(p => p.padre_id == e.Id).Select(m => m.Id).Any(),
                                   Children = getListDependencias.Data.Where(p => p.padre_id == e.Id ).Select(m => m.Id),
                               };
            return  Json(dependencias);
        }

        [HttpGet]
        public ActionResult InsSubDependencia(int? padreId)
        {
            return PartialView("InsDependencia", new Dependencia {padre_id = padreId });
        }

        [HttpGet]
        public ActionResult InsDependencia(int id)
        {
            var getDependenciaById = _DependenciaService.GetDependenciaById(id);

            if (!getDependenciaById.Success)
            {
                ModelState.AddModelError("Error", getDependenciaById.Message);
                return View(new Dependencia());
            }
            return PartialView("InsDependencia", new Dependencia { padre_id = getDependenciaById.Data.padre_id });
        }

        [HttpPost]
        public ActionResult InsDependencia(Dependencia dependencia)
        {
            var updDependencia = _DependenciaService.InsDependencia(dependencia);
            return Json(updDependencia);
        }

        [HttpGet]
        public ActionResult UpdDependencia(int id)
        {
            var getDependenciaById = _DependenciaService.GetDependenciaById(id);

            if (!getDependenciaById.Success)
            {
                ModelState.AddModelError("Error", getDependenciaById.Message);
                return View(new Dependencia());
            }
            return PartialView(getDependenciaById.Data);
        }

        [HttpPost]
        public ActionResult UpdDependencia(Dependencia dependencia)
        {
            var updDependencia = _DependenciaService.UpdDependencia(dependencia);
            return Json(updDependencia);
        }

        [HttpPost]
        public ActionResult DelDependencia(int id)
        {
            var delDependencia = _DependenciaService.DelDependencia(id);
            return Json(delDependencia);
        }

    }
}