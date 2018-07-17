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
    public class CargosController : Controller
    {
        ICargoDAOService _CargoService;
        public CargosController(ICargoDAOService CargoService)
        {
            _CargoService = CargoService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListCargos([DataSourceRequest] DataSourceRequest request)
        {
            var getListCargos = _CargoService.GetListCargos();

            if (!getListCargos.Success)
            {
                ModelState.AddModelError("Error", getListCargos.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Cargos = getListCargos.Data as List<Cargo>;
            return Json(Cargos.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdCargo(int id)
        {
            var getCargoById = _CargoService.GetCargoById(id);

            if (!getCargoById.Success)
            {
                ModelState.AddModelError("Error", getCargoById.Message);
                return View(new Cargo());
            }
            return PartialView(getCargoById.Data);
        }

        [HttpPost]
        public ActionResult UpdCargo(Cargo Cargo)
        {
            var updCargo = _CargoService.UpdCargo(Cargo);
            return new JsonResult(updCargo);
        }

        [HttpGet]
        public ActionResult InsCargo()
        {
            return PartialView(new Cargo());
        }

        [HttpPost]
        public ActionResult InsCargo(Cargo Cargo)
        {
            var insCargo = _CargoService.InsCargo(Cargo);
            return new JsonResult(insCargo);
        }



    }
}