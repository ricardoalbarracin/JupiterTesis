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
    public class PersonController : Controller
    {
        IPersonDAOService _personService;
        IDocumentTypeDAOService _documentTypeService;
        IGenderDAOService _genderService;

        public PersonController(IPersonDAOService personaService, IDocumentTypeDAOService documentTypeService, IGenderDAOService genderService)
        {
            _personService = personaService;
            _documentTypeService = documentTypeService;
            _genderService = genderService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListPersons([DataSourceRequest] DataSourceRequest request)
        {
            var getListPersons = _personService.GetListPersons();

            if (!getListPersons.Success)
            {
                ModelState.AddModelError("Error", getListPersons.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var persons = getListPersons.Data as List<Person>;
            return Json(persons.ToDataSourceResult(request));
        }

        public ActionResult GetListDocumentTypes([DataSourceRequest] DataSourceRequest request)
        {
            var getListTipoDocumento = _documentTypeService.GetListDocumentTypes();

            if (!getListTipoDocumento.Success)
            {
                ModelState.AddModelError("Error", getListTipoDocumento.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var tipoDocumentos = getListTipoDocumento.Data;
            return Json(tipoDocumentos.ToDataSourceResult(request));
        }

        public ActionResult GetListGenders([DataSourceRequest] DataSourceRequest request)
        {

            var getListSexo = _genderService.GetListGenders();

            if (!getListSexo.Success)
            {
                ModelState.AddModelError("Error", getListSexo.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Sexos = getListSexo.Data as List<Gender>;
            return Json(Sexos.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdPerson(int id)
        {
            var getPersonById = _personService.GetPersonById(id);

            if (!getPersonById.Success)
            {
                ModelState.AddModelError("Error", getPersonById.Message);
                return View(new Person());
            }
            return PartialView(getPersonById.Data);
        }

        [HttpPost]
        public ActionResult UpdPerson(Person person)
        {
            var updPerson = _personService.UpdPerson(person);
            return new JsonResult(updPerson);
        }

        [HttpGet]
        public ActionResult InsPerson()
        {
            return PartialView(new Person());
        }

        [HttpPost]
        public ActionResult InsPerson(Person person)
        {
            var insPerson = _personService.InsPerson(person);
            return new JsonResult(insPerson);
        }



    }
}