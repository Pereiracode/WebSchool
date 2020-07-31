using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace WebSite.Filters
{
    public class EnviaTokenFilterAttribute : System.Web.Mvc.ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = filterContext.HttpContext.Session["access_token"].ToString();
            string tipo = filterContext.HttpContext.Session["token_type"].ToString();
            try
            {
                var obj = (dynamic)filterContext.Controller;
                obj.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tipo, token);
            }
            catch
            {

            }
        }
    }
}