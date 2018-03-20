using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class MenuExtensions
{
    public static IHtmlContent MenuItem(
        this IHtmlHelper htmlHelper,
        string text,
        string action,
        string controller, 
        string icon,
        object routeValues = null, 
        object htmlAttributes = null
    )
    {

        var a = htmlHelper.ActionLink("", action, controller,routeValues,htmlAttributes) as TagBuilder;
        var routeData = htmlHelper.ViewContext.RouteData;
        var currentAction = routeData.Values["action"].ToString();
        var currentController = routeData.Values["controller"].ToString();
        a.AddCssClass("nav-link");
        if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
        {
            a.AddCssClass("active");
        }
        var i = new TagBuilder("i");
        i.AddCssClass(icon);
        
        a.InnerHtml.AppendHtml(i);
        a.InnerHtml.Append(text);
        return a;


    }
}