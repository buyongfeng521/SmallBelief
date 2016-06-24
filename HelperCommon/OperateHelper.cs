using Model;
using Model.StaticModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace HelperCommon
{
    public class OperateHelper
    {
        #region Http上下文相关属性
        public static HttpContext ContextHttp
        {
            get
            {
                return HttpContext.Current;
            }
        }

        static HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }

        static HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        #endregion



        /// <summary>
        /// 当前用户是否登陆
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            bool result = false;
            //1.(Session && Cookie)
            if (Session[OperateMsgModel.SessionLoginUser] == null)
            {
                if (Request.Cookies[OperateMsgModel.CookieLoginUser] != null)
                {
                    string ckUserID = Request.Cookies[OperateMsgModel.CookieLoginUser].Value;
                    int iUserID = 0;
                    if (!string.IsNullOrEmpty(ckUserID) && int.TryParse(ckUserID, out iUserID))
                    {
                        t_admin_user user = OperateContext.EFBLLSession.t_admin_userBLL.GetModelBy(u => u.ID == iUserID);
                        if (user != null)
                        {
                            Session[OperateMsgModel.SessionLoginUser] = user;
                            result = true;
                        }
                    }
                }
            }
            else
            {
                result = true;
            }

            //2.0 result
            return result;
        }

        /// <summary>
        /// 拼接两个字符串
        /// </summary>
        /// <param name="oneStr"></param>
        /// <param name="twoStr"></param>
        /// <returns></returns>
        public static string PathAppent(string oneStr, string twoStr)
        {
            return "/" + oneStr + twoStr;
        }
    }
}
