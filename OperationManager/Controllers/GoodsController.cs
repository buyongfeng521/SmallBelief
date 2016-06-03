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
        #endregion


        #region 商品分类
        [HttpGet]
        public ActionResult CategoryList()
        {
            return View();
        }
        #endregion


	}
}