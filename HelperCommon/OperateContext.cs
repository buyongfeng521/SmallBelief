using DapperBLL;
using EFBLL;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HelperCommon
{
    public class OperateContext
    {
        static EFBLLSession _EFBLLSession;
        public static EFBLLSession EFBLLSession
        {
            get 
            {
                return new EFBLLSession();
            }
            set
            {
                _EFBLLSession = value;
            }
        }




        #region Helper Ajax
        public static ActionResult RedirectAjax(string status, string msg, object data, string backurl)
        {
            AjaxMsg ajax = new AjaxMsg()
            {
                Status = status,
                Msg = msg,
                Data = data,
                BackUrl = backurl
            };
            JsonResult res = new JsonResult();
            res.Data = ajax;
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
        #endregion


        


    }
}
