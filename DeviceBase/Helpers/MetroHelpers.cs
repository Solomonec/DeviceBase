﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace MetroSupport.Helpers
{
    public static class MetroHelpers
    {

    public static HtmlString MenuLink(this HtmlHelper html, string linkText, string actionName, string controllerName, string id, 
                                      string defaultlink, string currentlink )
        {
            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];
            string currentDevice = HttpContext.Current.Request.QueryString["devicetype"];
            bool isActiveLink = string.Equals(currentAction, actionName, StringComparison.CurrentCultureIgnoreCase) &&
                                string.Equals(currentController, controllerName, StringComparison.CurrentCultureIgnoreCase) && string.Equals(currentDevice, id, StringComparison.CurrentCultureIgnoreCase);

            return html.ActionLink(
                linkText,
                actionName,
                controllerName,
                routeValues: new { devicetype = id},
                htmlAttributes: isActiveLink ? new { @class = currentlink } : new { @class = defaultlink });
        }

    }
}