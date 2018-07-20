using Core.Models.TRANS;
using Core.Services.TRANS;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("TRANS")]
    public class ProyectosController : Controller
    {
        IProyectoDAOService _ProyectoService;
        public ProyectosController(IProyectoDAOService ProyectoService)
        {
            _ProyectoService = ProyectoService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListProyectos([DataSourceRequest] DataSourceRequest request)
        {
            var getListProyectos = _ProyectoService.GetListProyectos();

            if (!getListProyectos.Success)
            {
                ModelState.AddModelError("Error", getListProyectos.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Proyectos = getListProyectos.Data as List<Proyecto>;
            return Json(Proyectos.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdProyecto(int id)
        {
            var getProyectoById = _ProyectoService.GetProyectoById(id);

            if (!getProyectoById.Success)
            {
                ModelState.AddModelError("Error", getProyectoById.Message);
                return View(new Proyecto());
            }
            return PartialView(getProyectoById.Data);
        }

        [HttpPost]
        public ActionResult UpdProyecto(Proyecto Proyecto)
        {
            var updProyecto = _ProyectoService.UpdProyecto(Proyecto);
            return new JsonResult(updProyecto);
        }

        [HttpGet]
        public ActionResult InsProyecto()
        {
            return PartialView(new Proyecto());
        }

        [HttpPost]
        public ActionResult InsProyecto(Proyecto Proyecto)
        {
            var insProyecto = _ProyectoService.InsProyecto(Proyecto);
            return new JsonResult(insProyecto);
        }



    }
}