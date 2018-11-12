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

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("PARAM")]
    [HasPermission("ADMIN")]
    public class StudentController : Controller
    {

        IStudentDAOService _studentService;

        IStudentService _studentBusinessService;

        public StudentController(IStudentDAOService studentService, IStudentService studentBusinessService)
        {
            _studentService = studentService;
            _studentBusinessService = studentBusinessService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListStudents([DataSourceRequest] DataSourceRequest request)
        {
            var getListStudents = _studentService.GetListStudents();

            if (!getListStudents.Success)
            {
                ModelState.AddModelError("Error", getListStudents.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var students = getListStudents.Data ;
            return Json(students.ToDataSourceResult(request));
        }

        public IActionResult UpdateStudent(int id)
        {
            var result = _studentBusinessService.GetStudentEditById(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            return PartialView(result.Data);
        }

        [HttpPost]
        public JsonResult FinishUpdate(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _studentBusinessService.UpdStudentPerson(secctions);
            return new JsonResult(result);
        }

        public IActionResult CreateStudent()
        {
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            return PartialView();
        }

        [HttpPost]
        public JsonResult FinishCreate(DataSections dataSections)
        {
            var secctions = this.ConvertSectionsToModels(dataSections.Sections);
            var result = _studentBusinessService.InsStudentPerson(secctions);
            return new JsonResult(result);
        }
    }
}