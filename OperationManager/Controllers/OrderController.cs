using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using Model.ViewModel;
using OperationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using Common;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace OperationManager.Controllers
{
    [LoginCheck]
    public class OrderController : Controller
    {

        #region 购物车
        public ActionResult CartList(string keywords = "")
        {
            //1.0 where
            List<t_cart> listCart = OperateContext.EFBLLSession.t_cartBLL.GetListBy(c => c.t_user.user_name.Contains(keywords) || c.t_user.user_phone.Contains(keywords), c => c.user_id);
            List<int> listUserID = listCart.Select(c=>(int)c.user_id).Distinct().ToList();
            List<t_user> listUsers = OperateContext.EFBLLSession.t_userBLL.GetListBy(u=>listUserID.Contains((int)u.ID));
            //2.0 result
            ViewBag.ListUser = listUsers;
            ViewBag.Keywords = keywords;
            return View(listCart);
        } 
        #endregion



        #region 订单
        public ActionResult OrderList(int? index = 1, string keywords = "",int ddlOrderStatus = 0,int ddlOrderType = 0)
        {
            //1.0 where
            Expression<Func<t_order_info, bool>> where = o => (o.consignee.Contains(keywords) || o.order_sn.Contains(keywords));
            //a OrderType
            if (ddlOrderType == (int)Enums.OrderTypePlus.普通订单)
            {
                where = where.And(o=>o.order_type == 0);
            }
            else if (ddlOrderType == (int)Enums.OrderTypePlus.预购订单)
            {
                where = where.And(o=>o.order_type == 1);
            }
            //b OrderStatus
            if (ddlOrderStatus == (int)Enums.OrderStatus.待付款)
            {
                where = where.And(o=>o.order_status == 1 && o.pay_status == 0);
            }
            else if (ddlOrderStatus == (int)Enums.OrderStatus.已取消)
            {
                where = where.And(o=>o.order_status == 2 && o.pay_status == 0);
            }
            else if (ddlOrderStatus == (int)Enums.OrderStatus.配送中)
            {
                where = where.And(o => o.order_status == 1 && o.pay_status == 1);
            }
            else if (ddlOrderStatus == (int)Enums.OrderStatus.待评价)
            {
                where = where.And(o => o.order_status == 3 && o.pay_status == 1);
            }
            else if (ddlOrderStatus == (int)Enums.OrderStatus.已完成)
            {
                where = where.And(o => o.order_status == 4 && o.pay_status == 1);
            }
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
            ViewBag.ListOrderType = SelectHelper.GetEnumSelectListItem(Enums.OrderTypePlus.全部类型, ddlOrderType.ToString());
            ViewBag.ListOrderStatus = SelectHelper.GetEnumSelectListItem(Enums.OrderStatus.全部订单, ddlOrderStatus.ToString());

            return View(mPage);
        }

        [HttpGet]
        public ActionResult OrderStatistics(int? index = 1,string keywords = "", int ddlCatType = -1)
        { 
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.goods_name.Contains(keywords);
            if (ddlCatType != -1)
            {
                where = where.And(g => g.t_category.cat_type == ddlCatType);
            }
            //2.0 do
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;

            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(pageIndex, pageSize, where, g => g.goods_id);
            List<OrderStatisticsVM> listVM = DTOHelper.Map<List<OrderStatisticsVM>>(listGoods);

            
            PagedList<OrderStatisticsVM> mPage = listVM.AsQueryable().ToPagedList(pageIndex, pageSize);


            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);

            //3.0 result
            ViewBag.Keywords = keywords;
            ViewBag.CatTypeSelList = SelectHelper.GetCategoryTypeSelListPlus(ddlCatType.ToString());

            return View(mPage);

        }

        [HttpGet]
        public ActionResult OrderStatisticsExcel(string keywords = "", int ddlCatType = -1)
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.goods_name.Contains(keywords);
            if (ddlCatType != -1)
            {
                where = where.And(g => g.t_category.cat_type == ddlCatType);
            }
            //2.0 do
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(where, g => g.goods_id);
            List<OrderStatisticsVM> listVM = DTOHelper.Map<List<OrderStatisticsVM>>(listGoods);


            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("统计");

            //给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow rowHeader = sheet.CreateRow(0);
            rowHeader.CreateCell(0).SetCellValue("编号");
            rowHeader.CreateCell(1).SetCellValue("商品名称");
            rowHeader.CreateCell(2).SetCellValue("商品库存");
            rowHeader.CreateCell(3).SetCellValue("今日下单商品数");

            ////将数据逐步写入sheet1各个行
            for (int i = 0; i < listVM.Count; i++)
            {
                NPOI.SS.UserModel.IRow rowtemp = sheet.CreateRow(i + 1);
                rowtemp.CreateCell(0).SetCellValue((i + 1).ToString());
                rowtemp.CreateCell(1).SetCellValue(listVM[i].goods_name.ToString());
                rowtemp.CreateCell(2).SetCellValue(listVM[i].goods_number.ToString());
                rowtemp.CreateCell(3).SetCellValue(listVM[i].order_goods_count.ToString());
            }
            // 写入到客户端 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            ms.Seek(0, SeekOrigin.Begin);
            DateTime dt = DateTime.Now;
            string dateTime = dt.ToString("yyMMddHHmmssfff");
            string fileName = "统计" + dateTime + ".xls";
            return File(ms, "application/vnd.ms-excel", fileName);
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
                    vm.order_status_content = ContentHelper.GetOrderStatusMsg(order.order_status,order.pay_status);
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

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult OrderCancel(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            //t_order_info t_order = 
            t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_id == id && o.order_status == 1 && o.pay_status == 0);
            if (order != null)
            {
                //1.0 商品库存
                List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == order.order_id);
                listOrderGoods.ForEach(item => {
                    t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(G=>G.goods_id == item.goods_id);
                    if (goods != null)
                    {
                        goods.goods_lock_number = (goods.goods_lock_number - item.goods_number) < 0 ? 0 : (goods.goods_lock_number - item.goods_number);
                        OperateContext.EFBLLSession.t_goodsBLL.Modify(goods);
                    }
                });
                //2.0 订单状态
                order.order_status = 2;
                if (OperateContext.EFBLLSession.t_order_infoBLL.Modify(order))
                {
                    ajax.Msg = CommonBasicMsg.OrderCancelSuc;
                    ajax.Status = "ok";
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.OrderCommentErr;
                }

            }
            else
            {
                ajax.Msg = "订单不存在或此状态订单不允许操作";
            }


            return Json(ajax);
        }

        [HttpPost]
        public ActionResult ShippingEnd(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o=>o.order_id == id && o.order_status == 1 && o.pay_status == 1);
            if (order != null)
            {
                order.order_status = 3;
                if (OperateContext.EFBLLSession.t_order_infoBLL.Modify(order))
                {
                    ajax.Msg = CommonBasicMsg.OperateSuc;
                    ajax.Status = "ok";
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.OperateFail;
                }
            }
            else
            {
                ajax.Msg = "订单不存在或此状态订单不允许操作";
            }

            return Json(ajax);
        }

        #endregion

	}
}