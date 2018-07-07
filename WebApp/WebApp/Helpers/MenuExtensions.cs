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
        
        var i = new TagBuilder("i");
        i.AddCssClass("sidebar-nav-item-icon " + icon);
        
        a.InnerHtml.AppendHtml(i);
        a.InnerHtml.Append(text);
        var li = new TagBuilder("li");
        li.InnerHtml.AppendHtml(a);
        if (string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase))
        {
            li.AddCssClass("active");
        }
        return li ;


    }
}