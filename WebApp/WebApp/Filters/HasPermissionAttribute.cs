﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using Core.Models.SEG;
using Microsoft.AspNetCore.Authentication;
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
            if (usuario?.Permissions?.Count == null)
            {
                context.HttpContext.Response.Redirect("/Account/Login", false);
            }
            if(usuario != null && !usuario.Permissions.Where(p=> p.Code == _permissionsString).Select(p => p.Code).Any())
            {
                context.Result = new UnauthorizedResult();
            }
        }
	}

    public class LoginActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var usuario = context.HttpContext.Session.GetUser();
            if (context.HttpContext.User.Identity.IsAuthenticated  && usuario == null)
            {
                context.HttpContext.SignOutAsync();
                context.HttpContext.Response.Redirect("/Account/Login", false);
            }
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }
}
