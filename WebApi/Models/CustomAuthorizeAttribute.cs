using HelperCommon;
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
            string token = HttpContext.Current.Request["token"];
            if (string.IsNullOrEmpty(token) || OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.token == token) <= 0)
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