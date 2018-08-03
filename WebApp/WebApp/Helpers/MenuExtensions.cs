using System;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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

    public static Microsoft.AspNetCore.Html.HtmlString EnumToString<T>(this IHtmlHelper helper)
    {
        var values = Enum.GetValues(typeof(T)).Cast<int>();
        var enumDictionary = values.ToDictionary(value => Enum.GetName(typeof(T), value)); return new Microsoft.AspNetCore.Html.HtmlString(JsonConvert.SerializeObject(enumDictionary));
    }
}