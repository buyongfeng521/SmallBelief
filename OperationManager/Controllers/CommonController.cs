using HelperCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult UploadImages(HttpPostedFileBase goods_img = null)
        {
            string result = "";

            HttpPostedFileBase file = HttpContext.Request.Files[0];
            result = ConfigurationManager.AppSettings["Domain"] + UploadHelper.UploadImage(file);
            
            return Content(result);
        }
	}
}