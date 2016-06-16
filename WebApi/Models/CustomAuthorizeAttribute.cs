using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace WebApi.Models
{
    public class CustomAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //用户判断
            string v = HttpContext.Current.Request["token"];
            if (string.IsNullOrEmpty(v))
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            HttpContext.Current.Response.Redirect("/Api/Common/Error");
        }

    }
}