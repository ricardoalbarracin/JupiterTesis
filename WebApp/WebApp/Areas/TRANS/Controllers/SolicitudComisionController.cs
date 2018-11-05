using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Areas.TRANS.Controllers
{
    [Area("TRANS")]
    public class SolicitudComisionController : Controller
    {
        // GET: /<controller>/
        public ActionResult SolicitudComision()
        {
            return View();
        }

        public ActionResult GetListComisiones([DataSourceRequest] DataSourceRequest request)
        {
            //var getListProyectos = _ProyectoService.GetListProyectos();

            //if (!getListProyectos.Success)
            //{
            //    ModelState.AddModelError("Error", getListProyectos.Message);
            //    return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            //}
            //var Proyectos = getListProyectos.Data;
            return null;//json(Proyectos.ToDataSourceResult(request));
        }
    }
}
