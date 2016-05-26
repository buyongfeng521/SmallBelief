using HelperCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Models
{
    public class LoginCheckAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //a 校验用户是否登录
            if (!OperateHelper.IsLogin())
            {
                if (filterContext.ActionDescriptor.IsDefined(typeof(AjaxRequestAttribute), false)
                    || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AjaxRequestAttribute), false))
                {
                    filterContext.Result = OperateContext.RedirectAjax("nologin", "登陆失效", null, "/Admin/Home/Login");
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Login");
                }
                return;
            }

        }
    }
}