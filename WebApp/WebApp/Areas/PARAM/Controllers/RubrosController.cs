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
    public class RubrosController : Controller
    {
        IRubroDAOService _RubroService;
        public RubrosController(IRubroDAOService RubroService)
        {
            _RubroService = RubroService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListRubros([DataSourceRequest] DataSourceRequest request)
        {
            var getListRubros = _RubroService.GetListRubros();

            if (!getListRubros.Success)
            {
                ModelState.AddModelError("Error", getListRubros.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Rubros = getListRubros.Data as List<Rubro>;
            return Json(Rubros.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdRubro(int id)
        {
            var getRubroById = _RubroService.GetRubroById(id);

            if (!getRubroById.Success)
            {
                ModelState.AddModelError("Error", getRubroById.Message);
                return View(new Rubro());
            }
            return PartialView(getRubroById.Data);
        }

        [HttpPost]
        public ActionResult UpdRubro(Rubro Rubro)
        {
            var updRubro = _RubroService.UpdRubro(Rubro);
            return new JsonResult(updRubro);
        }

        [HttpGet]
        public ActionResult InsRubro()
        {
            return PartialView(new Rubro());
        }

        [HttpPost]
        public ActionResult InsRubro(Rubro Rubro)
        {
            var insRubro = _RubroService.InsRubro(Rubro);
            return new JsonResult(insRubro);
        }



    }
}