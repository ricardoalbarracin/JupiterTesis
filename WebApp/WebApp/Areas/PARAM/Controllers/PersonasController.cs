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
        IPersonDAOService _personaService;
        public PersonasController(IPersonDAOService personaService)
        {
            _personaService = personaService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListPersonas([DataSourceRequest] DataSourceRequest request)
        {
            var getListPersonas = _personaService.GetListPersons();

            if (!getListPersonas.Success)
            {
                ModelState.AddModelError("Error", getListPersonas.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var personas = getListPersonas.Data as List<Person>;
            return Json(personas.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdPersona(int id)
        {
            var getPersonaById = _personaService.GetPersonById(id);

            if (!getPersonaById.Success)
            {
                ModelState.AddModelError("Error", getPersonaById.Message);
                return View(new Person());
            }
            return PartialView(getPersonaById.Data);
        }

        [HttpPost]
        public ActionResult UpdPersona(Person persona)
        {
            var updPersona = _personaService.UpdPerson(persona);
            return new JsonResult(updPersona);
        }

        [HttpGet]
        public ActionResult InsPersona()
        {
            return PartialView(new Person());
        }

        [HttpPost]
        public ActionResult InsPersona(Person persona)
        {
            var insPersona = _personaService.InsPerson(persona);
            return new JsonResult(insPersona);
        }



    }
}