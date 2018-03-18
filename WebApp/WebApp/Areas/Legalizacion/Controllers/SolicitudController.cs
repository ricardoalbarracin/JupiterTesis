using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Legalizacion.Controllers
{
    [Area("Legalizacion")]
    public class SolicitudController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}