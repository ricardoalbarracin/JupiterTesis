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
        ITiposDocumentoDAOService _tiposDocumentoService;
        ISexoDAOService _sexo;

        public PersonasController(IPersonaDAOService personaService, ITiposDocumentoDAOService tiposDocumentoService, ISexoDAOService sexoDAO)
        {
            _personaService = personaService;
            _tiposDocumentoService = tiposDocumentoService;
            _sexo = sexoDAO;
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
            persona.FechaNacimiento = DateTime.Now;
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
            persona.FechaNacimiento = DateTime.Now;           
            var insPersona = _personaService.InsPersona(persona);
            return new JsonResult(insPersona);
        }

        public ActionResult GetListTipoDocumento([DataSourceRequest] DataSourceRequest request)
        {
            var getListTipoDocumento = _tiposDocumentoService.GetListTiposDocumento();

            if (!getListTipoDocumento.Success)
            {
                ModelState.AddModelError("Error", getListTipoDocumento.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var tipoDocumentos = getListTipoDocumento.Data;
            return Json(tipoDocumentos.ToDataSourceResult(request));
        }

        public ActionResult GetListSexo([DataSourceRequest] DataSourceRequest request)
        {
           
            var getListSexo = _sexo.GetListSexos();

            if (!getListSexo.Success)
            {
                ModelState.AddModelError("Error", getListSexo.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Sexos = getListSexo.Data as List<Sexos>;
            return Json(Sexos.ToDataSourceResult(request));
        }


    }
}