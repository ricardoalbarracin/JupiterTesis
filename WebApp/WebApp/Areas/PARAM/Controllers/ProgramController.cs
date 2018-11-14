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
    public class ProgramController : Controller
    {
        IProgramDAOService _programService;


        public ProgramController(IProgramDAOService programaService)
        {
            _programService = programaService;

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetListPrograms([DataSourceRequest] DataSourceRequest request)
        {
            var getListPrograms = _programService.GetListPrograms();

            if (!getListPrograms.Success)
            {
                ModelState.AddModelError("Error", getListPrograms.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var programs = getListPrograms.Data as List<models.Program>;
            return Json(programs.ToDataSourceResult(request));
        }

       

       

        [HttpGet]
        public ActionResult UpdProgram(int id)
        {
            var getProgramById = _programService.GetProgramById(id);

            if (!getProgramById.Success)
            {
                ModelState.AddModelError("Error", getProgramById.Message);
                return View(new models.Program());
            }
            return PartialView(getProgramById.Data);
        }

        [HttpPost]
        public ActionResult UpdProgram(models.Program program)
        {
            var updProgram = _programService.UpdProgram(program);
            return new JsonResult(updProgram);
        }

        [HttpGet]
        public ActionResult InsProgram()
        {
            return PartialView(new models.Program());
        }

        [HttpPost]
        public ActionResult InsProgram(models.Program program)
        {
            var insProgram = _programService.InsProgram(program);
            return new JsonResult(insProgram);
        }



    }
}