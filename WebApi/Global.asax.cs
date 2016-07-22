using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using HelperCommon;

namespace WebApi
{
    /// <summary>
    /// Global
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// Start
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //EF预热
            OperateContext.EFBLLSession.t_admin_userBLL.GetModelBy(u => u.ID > 0);
        }

        /// <summary>
        /// Init
        /// </summary>
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        /// <summary>
        /// MvcAppication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(
                SessionStateBehavior.Required);
        }

    }
}