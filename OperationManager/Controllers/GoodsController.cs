using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class GoodsController : Controller
    {

        #region 商品

        [HttpGet]
        public ActionResult GoodsList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GoodsAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoodsAdd(string goods_name)
        {
            return View();
        }

        [HttpGet]
        public ActionResult GoodsEdit(int id = 0)
        {
            return View();
        }
        [HttpPost]
        public ActionResult GoodsEdit()
        {
            return View();
        }

        public ActionResult GoodsDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            { 
                
            }

            return Json(ajax);
        }

        #endregion


        #region 商品分类
        [HttpGet]
        public ActionResult CategoryList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CategoryDelete(int id = 0)
        { 
            AjaxMsg ajax = new AjaxMsg();
            return Json(ajax);
        }
        #endregion


	}
}