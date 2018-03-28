﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Helpers;

namespace WebApp.Areas.SEG.Controllers
{
    [Area("SEG")]
    public class UsuariosController : Controller
    {
        IUsuarioService _usuarioService;
       
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult GetListUsuarios([DataSourceRequest] DataSourceRequest request)
        {
            var getListUsuarios = _usuarioService.ListUsuarios();

            if (!getListUsuarios.Success)
            {
                ModelState.AddModelError("Error", getListUsuarios.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var usuarios = getListUsuarios.Data as List<UsuarioIdentity>;
            return Json(usuarios.ToDataSourceResult(request));

        }

        public IActionResult ActualizarUsuario(int id)
        {
            var result = _usuarioService.UsuarioEditById(id);
            ViewBag.Container =ControllerContext.RouteData.Values["action"].ToString();
            return View(result.Data);
        }

        [HttpPost]
        public JsonResult Finish(DataSections dataSections)
        {
            var a = this.ConvertSectionsToModels(dataSections.Sections);
            return new JsonResult("ok");
        }

        public IActionResult AgregarUsuarioRole()
        {
            return PartialView();
        }
        


    }
}