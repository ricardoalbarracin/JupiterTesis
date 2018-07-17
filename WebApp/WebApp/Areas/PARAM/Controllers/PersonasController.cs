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
    public class PersonasController : Controller
    {
        IPersonaDAOService _personaService;
        public PersonasController(IPersonaDAOService personaService)
        {
            _personaService = personaService;
        }
        public ActionResult Index()
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

        [HttpGet]
        public ActionResult UpdPersona(int id)
        {
            var getPersonaById = _personaService.GetPersonaById(id);

            if (!getPersonaById.Success)
            {
                ModelState.AddModelError("Error", getPersonaById.Message);
                return View(new Persona());
            }
            return PartialView(getPersonaById.Data);
        }

        [HttpPost]
        public ActionResult UpdPersona(Persona persona)
        {
            var updPersona = _personaService.UpdPersona(persona);
            return new JsonResult(updPersona);
        }

        [HttpGet]
        public ActionResult InsPersona()
        {
            return PartialView(new Persona());
        }

        [HttpPost]
        public ActionResult InsPersona(Persona persona)
        {
            var insPersona = _personaService.InsPersona(persona);
            return new JsonResult(insPersona);
        }



    }
}