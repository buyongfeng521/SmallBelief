using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class CouponController : Controller
    {
        //
        // GET: /Coupon/
        public ActionResult CouponList(string keywords = "")
        {
            List<t_coupon> listCoupon = OperateContext.EFBLLSession.t_couponBLL.GetListByDesc(c => c.is_del == false && c.coupon_name.Contains(keywords), c => c.create_time);

            ViewBag.Keywords = keywords;
            return View(listCoupon);
        }

        [HttpPost]
        public ActionResult CouponAdd(string coupon_name = "", decimal coupon_amount = 0, int valid_days = 0)
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrEmpty(coupon_name))
            {
                ajax.Msg = "优惠券名称不能为空";
                return Json(ajax);
            }
            if (coupon_amount <= 0)
            {
                ajax.Msg = "优惠券金额不正确";
                return Json(ajax);
            }
            if (valid_days <= 0)
            {
                ajax.Msg = "有效条数不正确";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_couponBLL.GetCountBy(c => c.is_del == false && c.coupon_name == coupon_name.Trim()) > 0)
            {
                ajax.Msg = "已存在此优惠券名称";
                return Json(ajax);
            }
            //2.0 do
            t_coupon model = new t_coupon() 
            {
                coupon_name = coupon_name.Trim(),
                coupon_amount = coupon_amount,
                valid_days = valid_days,
                is_del = false,
                create_time = DateTime.Now
            };
            if (OperateContext.EFBLLSession.t_couponBLL.Add(model))
            {
                ajax.Status = "ok";
                ajax.Msg = CommonBasicMsg.AddSuc;
            }
            else
            {
                ajax.Msg = CommonBasicMsg.AddFail;
            }
            //3.0 result
            return Json(ajax);
        }


        [HttpPost]
        public ActionResult CouponEdit(int coupon_id = 0, string coupon_name = "", decimal coupon_amount = 0, int valid_days = 0)
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrEmpty(coupon_name))
            {
                ajax.Msg = "优惠券名称不能为空";
                return Json(ajax);
            }
            if (coupon_amount <= 0)
            {
                ajax.Msg = "优惠券金额不正确";
                return Json(ajax);
            }
            if (valid_days <= 0)
            {
                ajax.Msg = "有效条数不正确";
                return Json(ajax);
            }

            t_coupon editModel = OperateContext.EFBLLSession.t_couponBLL.GetModelBy(c=>c.is_del == false && c.coupon_id == coupon_id);
            if (editModel == null)
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
                return Json(ajax);
            }

            if (OperateContext.EFBLLSession.t_couponBLL.GetCountBy(c => c.coupon_id != editModel.coupon_id && c.is_del == false && c.coupon_name == coupon_name.Trim()) > 0)
            {
                ajax.Msg = "已存在此优惠券名称";
                return Json(ajax);
            }
            //2.0 do
            editModel.coupon_name = coupon_name.Trim();
            editModel.coupon_amount = coupon_amount;
            editModel.valid_days = valid_days;
            if (OperateContext.EFBLLSession.t_couponBLL.Modify(editModel))
            {
                ajax.Status = "ok";
                ajax.Msg = CommonBasicMsg.EditSuc;
            }
            else
            {
                ajax.Msg = CommonBasicMsg.EditFail;
            }
            //3.0 result
            return Json(ajax);
        }


	}
}