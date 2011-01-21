using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace UrlBuilder
{
    public static class UrlHelperExtensions
    {
        public static string For<TController>(this UrlHelper helper, Expression<Func<TController, ActionResult>> controllerAction)
        {
            var methodName = controllerAction.GetMethodName();
            var valueDictionary = new RouteValueDictionary();
            controllerAction.ForEachParameter((paramInfo, value) => valueDictionary.Add(paramInfo.Name, value));
            var controllerClassName = typeof(TController).Name;
            var controllerName = controllerClassName.Remove(controllerClassName.Length - "Controller".Length);
            return helper.Action(methodName, controllerName, valueDictionary);
        }
    }
}
