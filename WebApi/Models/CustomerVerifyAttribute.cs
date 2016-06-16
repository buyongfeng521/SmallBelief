using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Models
{
    public class CustomerVerifyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //+ 钉钉 免登录
            string userToken = filterContext.HttpContext.Request["token"];
            if (!string.IsNullOrEmpty(userToken))
            {

            }
            else
            {
                filterContext.Result = new RedirectResult("/Api/Common/Error");
            }

        }

    }

}