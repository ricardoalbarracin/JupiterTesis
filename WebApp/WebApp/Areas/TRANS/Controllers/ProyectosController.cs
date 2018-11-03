using Core.Models.TRANS;
using Core.Services.TRANS;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("TRANS")]
    public class ProyectosController : Controller
    {
        IProyectoDAOService _ProyectoService;
        IProyectoServiceBusiness _ProyectoServiceBusiness;
        public ProyectosController(IProyectoDAOService ProyectoService, IProyectoServiceBusiness proyectoServiceBusiness)
        {
            _ProyectoService = ProyectoService;
            _ProyectoServiceBusiness = proyectoServiceBusiness;
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
            var Proyectos = getListProyectos.Data;
            return Json(Proyectos.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult ActualizarProyecto(int id)
        {
            var getProyectoById = _ProyectoServiceBusiness.GetProyectoById(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            if (!getProyectoById.Success)
            {
                ModelState.AddModelError("Error", getProyectoById.Message);
                return View(new ProyectoEdit());
            }
            return PartialView( getProyectoById.Data );
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
            return  PartialView(getProyectoById.Data);
        }

        [HttpPost]
        public ActionResult UpdProyecto(Proyecto proyecto)
        {
            
            var updProyecto = _ProyectoService.UpdProyecto(proyecto);
            return new JsonResult(updProyecto);
        }

        [HttpGet]
        public ActionResult InsProyecto()
        {
            return PartialView(new Proyecto());
        }


        [HttpPost]
        public ActionResult InsProyecto(Proyecto proyecto)
        {
            proyecto.FechaCreacion = DateTime.Now;
            var insProyecto = _ProyectoService.InsProyecto(proyecto);
            return new JsonResult(insProyecto);
        }

        [HttpGet]
        public ActionResult RubrosProyecto(int id)
        {
            Proyecto proyecto = new Proyecto();
            var getProyecto = _ProyectoService.GetValoresProyectos(id);
            proyecto = getProyecto.Data;
            return PartialView(proyecto);
        }

        public ActionResult GetListRubrosProyecto([DataSourceRequest] DataSourceRequest request,int id)
        {
            var getListRubrosByProyectoId = _ProyectoService.GetListRubrosByProyectoId(id);

            if (!getListRubrosByProyectoId.Success)
            {
                ModelState.AddModelError("Error", getListRubrosByProyectoId.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var rubrosProyecto = getListRubrosByProyectoId.Data;
            return Json(rubrosProyecto.ToDataSourceResult(request));
        }


        [HttpGet]
        public ActionResult UpdRubroProyecto(int id)
        {
            var getRubroProyecto = _ProyectoService.GetRubroProyecto(id);         
            if (!getRubroProyecto.Success)
            {
                ModelState.AddModelError("Error", getRubroProyecto.Message);
                return View(new ProyectoRubro());
            }
            return PartialView(getRubroProyecto.Data);
        }

        [HttpPost]
        public ActionResult UpdRubroProyecto(ProyectoRubro rubroProyecto)
        {
            rubroProyecto.FechaModificacion = DateTime.Now;            
            var updProyecto = _ProyectoService.UpdRubroProyecto(rubroProyecto);
            return new JsonResult(updProyecto);
        }

        [HttpGet]
        public ActionResult InsRubroProyecto(int id)
        {

            var model = new ProyectoRubro {ProyectoId = id };
            return PartialView(model);
        }


        [HttpPost]
        public ActionResult InsRubroProyecto(ProyectoRubro rubroProyecto)
        {
            rubroProyecto.FechaModificacion = DateTime.Now;
            rubroProyecto.FechaCreacion = DateTime.Now;
            var insProyecto = _ProyectoService.InsRubroProyecto(rubroProyecto);
            return new JsonResult(insProyecto);
        }

    }
}