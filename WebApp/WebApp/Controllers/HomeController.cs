using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.SEG;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        
        
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult CustomAjaxBinding_Read([Kendo.Mvc.UI.DataSourceRequest] DataSourceRequest request)
        {
            IList<EmptyClass> list = new List<EmptyClass>();
            list.Add(new EmptyClass()
            {
                ProductName = "dasda"
            });
            list.Add(new EmptyClass()
            {
                ProductName = "XX"
            });
            list.Add(new EmptyClass()
            {
                ProductName = "mm"
            });

            var result = new Kendo.Mvc.UI.DataSourceResult()
            {
                Data = list,
                Total = 30
            };
            var a = Json(result); ;
            return a;

        }
        public ActionResult ActualizarPersona()
        {
            // Inicializaciones
            
            // Salida SUCCESS
            return PartialView();
        }
    }
}
