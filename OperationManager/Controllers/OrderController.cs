using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class OrderController : Controller
    {

        #region 购物车
        public ActionResult CartList()
        {
            return View();
        } 
        #endregion



        #region 订单
        public ActionResult OrderList()
        {
            return View();
        } 
        #endregion

	}
}