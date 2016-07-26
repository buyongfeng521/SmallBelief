using HelperCommon;
using Model;
using Model.ViewModel;
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

        /// <summary>
        /// 获得订单详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrderDetail(int id = 0)
        {
            OrderDetailViewModel vm = new OrderDetailViewModel();
            if (id > 0)
            {
                t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o=>o.order_id == id);
                if (order != null)
                {
                    vm.order_sn = order.order_sn;
                    vm.order_status_content = "";
                    vm.order_amount = (decimal)order.order_amount;
                    vm.add_time = ((DateTime)order.add_time).ToString("yyyy-MM-dd HH:mm:ss");
                    vm.consignee = order.consignee;
                    vm.mobile = order.mobile;
                    vm.address = order.address;
                    List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(g=>g.order_id == order.order_id);
                    vm.order_goods = DTOHelper.Map<List<OrderGoodsViewModel>>(listOrderGoods);
                }
            }

            return Json(vm, JsonRequestBehavior.AllowGet);
        }

        #endregion

	}
}