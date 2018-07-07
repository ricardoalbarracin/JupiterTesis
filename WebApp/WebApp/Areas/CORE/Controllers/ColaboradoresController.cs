using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.CORE;
using Core.Services.CORE;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.CORE.Controllers
{
    [Area("CORE")]
    public class ColaboradoresController : Controller
    {

        IColaboradorDAOService _colaboradorService;
        public ColaboradoresController(IColaboradorDAOService colaboradorService)
        {
            _colaboradorService = colaboradorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetListColaboradores([DataSourceRequest] DataSourceRequest request)
        {
            var getListPersonas = _colaboradorService.ListColaboradoresGrid();

            if (!getListPersonas.Success)
            {
                ModelState.AddModelError("Error", getListPersonas.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var personas = getListPersonas.Data as List<ColaboradorGrid>;
            return Json(personas.ToDataSourceResult(request));
        }
    }
}