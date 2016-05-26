using Common;
using HelperCommon;
using Model;
using Model.FormatModel;
using Model.StaticModel;
using OperationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class HomeController : Controller
    {
        [LoginCheck]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string user_name, string user_psw,string remember)
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrEmpty(user_name))
            {
                ajax.Msg = "请输入用户名";
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(user_psw))
            {
                ajax.Msg = "请输入密码";
                return Json(ajax);
            }

            //2.0 Data check
            t_admin_user user = OperateContext.EFBLLSession.t_admin_userBLL.GetModelBy(u=>u.user_name == user_name.Trim());
            if (user != null && user.user_psw == SecurityHelper.GetMD5(user_psw.Trim()))
            {
                Session[OperateMsgModel.SessionLoginUser] = user;
                if (remember == "on")
                {
                    //cookie
                    HttpCookie cookie = new HttpCookie(OperateMsgModel.CookieLoginUser, user.ID.ToString());
                    cookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Add(cookie);
                }

                ajax.Msg = "";
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = "用户名或密码错误";
            }


            return Json(ajax);
        }

        [LoginCheck]
        public ActionResult Logout()
        {
            Session[OperateMsgModel.SessionLoginUser] = null;
            Session.Clear();

            if (Request.Cookies[OperateMsgModel.CookieLoginUser] != null)
            {
                HttpCookie ck = Request.Cookies[OperateMsgModel.CookieLoginUser];
                ck.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(ck);
            }

            return RedirectToAction("Login");
        }

	}
}