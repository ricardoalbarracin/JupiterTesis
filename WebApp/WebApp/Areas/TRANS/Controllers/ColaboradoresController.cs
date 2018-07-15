using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Services.PARAM;
using Core.Services.TRANS;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.TRANS.Controllers
{
    [Area("TRANS")]
    public class ColaboradoresController : Controller
    {

        IColaboradorDAOService _colaboradorService;
        IPersonaDAOService _personaService;

        public ColaboradoresController(IColaboradorDAOService colaboradorService, IPersonaDAOService personaService)
        {
            _colaboradorService = colaboradorService;
            _personaService = personaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult GetListColaboradores([DataSourceRequest] DataSourceRequest request)
        {
            var getListPersonas = _personaService.GetListPersonas();

            if (!getListPersonas.Success)
            {
                ModelState.AddModelError("Error", getListPersonas.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var personas = getListPersonas.Data as List<Persona>;
            return Json(personas.ToDataSourceResult(request));
        }

        public IActionResult ActualizarColaborador(int id)
        {
            var result = _colaboradorService.GetColaboradorEditById(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            return PartialView(result.Data);
        }
    }
}