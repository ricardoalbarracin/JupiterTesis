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
        public PersonController(IPersonDAOService personService)
        {
            _personService = personService;
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