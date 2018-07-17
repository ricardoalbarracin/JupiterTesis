﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Core.Models.SEG;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Utils;

namespace WebApp.Fliters
{
	/// <summary>
	/// Filtro para determinar la existencia de un permiso
	/// </summary>
	public class HasPermissionAttribute : ActionFilterAttribute
	{
		private string _permissionsString;

		public HasPermissionAttribute(string permissionsString)
		{
			this._permissionsString = permissionsString;
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
            var usuario = context.HttpContext.Session.GetUser();
            if (usuario?.Permisos?.Count == null)
            {
                context.HttpContext.Response.Redirect("/Account/Login", false);
            }
            if(usuario != null && !usuario.Permisos.Where(p=> p.Sigla == _permissionsString).Select(p => p.Sigla).Any())
            {
                context.Result = new UnauthorizedResult();
            }
        }
	}
}
