using HelperCommon;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace OperationManager.Controllers
{
    public class OrderController : Controller
    {

        #region 购物车
        public ActionResult CartList(string keywords = "")
        {
            List<t_cart> listCart = OperateContext.EFBLLSession.t_cartBLL.GetListBy(c=>c.t_user.user_name.Contains(keywords),c=>c.user_id);

            ViewBag.Keywords = keywords;
            return View(listCart);
        } 
        #endregion



        #region 订单
        public ActionResult OrderList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_order_info, bool>> where = o => o.consignee.Contains(keywords);
            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_order_infoBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_order_info> listGoods = OperateContext.EFBLLSession.t_order_infoBLL.GetListByDesc(where, o => o.order_id);
            PagedList<t_order_info> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        } 
        #endregion

	}
}