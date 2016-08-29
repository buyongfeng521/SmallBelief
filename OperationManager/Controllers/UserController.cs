using HelperCommon;
using Model;
using Model.DTOModel;
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

namespace OperationManager.Controllers
{
    [LoginCheck]
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult UserList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_user, bool>> where = u => u.user_phone.Contains(keywords) || u.user_name.Contains(keywords);
            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_userBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_user> listGoods = OperateContext.EFBLLSession.t_userBLL.GetListByDesc(where, u => u.create_time);
            PagedList<t_user> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;
            ViewBag.CouponSelList = SelectHelper.GetCouponSelList();

            return View(mPage);
        }

        [HttpPost]
        public ActionResult ApplyCoupon(int hideUserID = 0, int ddlCouponID = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.ID == hideUserID);
            if (user != null)
            {
                t_coupon coupon = OperateContext.EFBLLSession.t_couponBLL.GetModelBy(c=>c.coupon_id == ddlCouponID);
                if (coupon != null)
                {
                    t_user_coupon addCoupon = new t_user_coupon()
                    {
                        user_id = user.ID,
                        coupon_id = coupon.coupon_id,
                        coupon_type = coupon.coupon_type,
                        coupon_img = coupon.coupon_img,
                        condition_amount = coupon.condition_amount,
                        coupon_amount = coupon.coupon_amount,
                        begin_time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                        end_time = DateTime.Parse(DateTime.Now.AddDays((int)coupon.valid_days).ToString("yyyy-MM-dd 00:00:00")),
                        is_use = false,
                        use_time = null
                    };
                    if (OperateContext.EFBLLSession.t_user_couponBLL.Add(addCoupon))
                    {
                        ajax.Msg = "分配成功";
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = "分配失败";
                    }
                }
                else
                {
                    ajax.Msg = "该优惠卷不存在";
                }
            }
            else
            {
                ajax.Msg = "该用户不存在";
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult ApplyCouponAll(int ddlCouponAllID = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            t_coupon coupon = OperateContext.EFBLLSession.t_couponBLL.GetModelBy(c => c.coupon_id == ddlCouponAllID);
            if (coupon != null)
            {
                List<t_user> listUser = OperateContext.EFBLLSession.t_userBLL.GetListBy(u => u.ID > 0);
                bool flag = true;
                listUser.ForEach(data =>
                {
                    t_user_coupon addCoupon = new t_user_coupon()
                    {
                        user_id = data.ID,
                        coupon_id = coupon.coupon_id,
                        coupon_type = coupon.coupon_type,
                        coupon_img = coupon.coupon_img,
                        condition_amount = coupon.condition_amount,
                        coupon_amount = coupon.coupon_amount,
                        begin_time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                        end_time = DateTime.Parse(DateTime.Now.AddDays((int)coupon.valid_days).ToString("yyyy-MM-dd 00:00:00")),
                        is_use = false,
                        use_time = null
                    };
                    if (!OperateContext.EFBLLSession.t_user_couponBLL.Add(addCoupon))
                    {
                        flag = false;
                    }
                });

                if (flag)
                {
                    ajax.Msg = "成功";
                    ajax.Status = "ok";
                }
            }
            else
            {
                ajax.Msg = "该优惠券不存在";
            }

            return Json(ajax);
        }

        [HttpGet]
        public ActionResult GetUserBy(int id = 0)
        {
            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.ID == id);
            UserDTO userDto = DTOHelper.Map<UserDTO>(user);
            userDto.create_time = ((DateTime)user.create_time).ToString("yyyy-MM-dd");
            userDto.last_login_time = ((DateTime)user.last_login_time).ToString("yyyy-MM-dd");

            return Json(userDto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUserCouponBy(int id = 0)
        {
            if (id > 0)
            {
                List<t_user_coupon> listCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetListByDesc(c => c.user_id == id, c => c.begin_time);
                if (listCoupon.Count > 0)
                {
                    return Json(DTOHelper.Map<List<UserCouponVM>>(listCoupon), JsonRequestBehavior.AllowGet);
                }
            }

            return null;
        }


	}
}