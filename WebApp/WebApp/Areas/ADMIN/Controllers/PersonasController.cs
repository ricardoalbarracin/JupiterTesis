using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.ADMIN;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.ADMIN;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Areas.ADMIN.Controllers
{
    [Area("ADMIN")]
    public class PersonasController : Controller
    {
        IPersonaDAOService _personaService;
        public PersonasController(IPersonaDAOService personaService)
        {
            _personaService = personaService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetListPersonas([DataSourceRequest] DataSourceRequest request)
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
        

    }
}