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
using WebApp.Fliters;
using WebApp.Helpers;
using models = Core.Models.PARAM;

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("PARAM")]
    [HasPermission("ADMIN")]
    public class SubjectController : Controller
    {
        ISubjectDAOService _subjectService;


        public SubjectController(ISubjectDAOService subjectaService)
        {
            _subjectService = subjectaService;
          
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListSubjects([DataSourceRequest] DataSourceRequest request)
        {
            var getListSubjects = _subjectService.GetListSubjects();

            if (!getListSubjects.Success)
            {
                ModelState.AddModelError("Error", getListSubjects.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var subjects = getListSubjects.Data as List<models.Subject>;
            return Json(subjects.ToDataSourceResult(request));
        }

        [HttpGet]
        public ActionResult UpdSubject(int id)
        {
            var getSubjectById = _subjectService.GetSubjectById(id);

            if (!getSubjectById.Success)
            {
                ModelState.AddModelError("Error", getSubjectById.Message);
                return View(new models.Subject());
            }
            return PartialView(getSubjectById.Data);
        }

        [HttpPost]
        public ActionResult UpdSubject(models.Subject subject)
        {
            var updSubject = _subjectService.UpdSubject(subject);
            return new JsonResult(updSubject);
        }

        [HttpGet]
        public ActionResult InsSubject()
        {
            return PartialView(new models.Subject());
        }

        [HttpPost]
        public ActionResult InsSubject(models.Subject subject)
        {
            var insSubject = _subjectService.InsSubject(subject);
            return new JsonResult(insSubject);
        }



    }
}