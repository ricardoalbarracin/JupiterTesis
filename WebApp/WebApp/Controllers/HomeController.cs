using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Services.SEG;
using Core.Services.Utils;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Fliters;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using WebApp.Utils;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        IEntitiesService _entitiesService;
        public HomeController(IEntitiesService entitiesService)
        {
            _entitiesService = entitiesService;
        }
        public IActionResult Index()
        {
            var a = HttpContext.Session.GetUser();
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

        public ActionResult ActualizarPersona()
        {
            // Inicializaciones
            
            // Salida SUCCESS
            return PartialView();
        }

        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect("~/Home");
        }


        public string GetEntity(string schema, string table)
        {
            var colums = _entitiesService.GetListColumnsTable(schema, table);
            var properties = $@"using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.{colums.Data[0].GetStringSchemaName().ToUpper()}
{{
        [Table(""{colums.Data[0].TableSchema}.{colums.Data[0].TableName}"")]
        public class {colums.Data[0].GetStringTableName()}
        {{" + Environment.NewLine;
            foreach (var item in colums.Data)
            {
                properties +=  item.PropertyWhitAnnotations + Environment.NewLine;
            }

            return  properties+ "}}";
        }

    }
}
