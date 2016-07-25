﻿using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using Model.StaticModel;
using OperationManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    //[LoginCheck]
    public class SettingsController : Controller
    {
        
        public ActionResult UserInfo()
        {
            return View();
        }


        #region Admin_User
        public ActionResult AdminUser(string keywords = "")
        {
            //1.0 where
            Expression<Func<t_admin_user, bool>> where = a => a.ID > 0 && a.ID != 1 && a.user_name.Contains(keywords);
            //2.0 result
            List<t_admin_user> listUser = OperateContext.EFBLLSession.t_admin_userBLL.GetListBy(where);
            ViewBag.Keywords = keywords;
            return View(listUser);
        }

        [HttpPost]
        public ActionResult AdminUserAdd(string user_name = "", string user_psw = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrEmpty(user_name.Trim()))
            {
                ajax.Msg = "用户名不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(user_psw.Trim()))
            {
                ajax.Msg = "密码不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_admin_userBLL.GetCountBy(a => a.user_name == user_name.Trim()) > 0)
            {
                ajax.Msg = OperateMsgModel.ExistsUserName;
                return Json(ajax);
            }
            //2.0 do
            t_admin_user addModel = new t_admin_user() 
            {
                user_name = user_name.Trim(),
                user_psw = SecurityHelper.GetMD5(user_psw.Trim()),
                create_time = DateTime.Now,
                last_login_time = DateTime.Now
            };
            if (OperateContext.EFBLLSession.t_admin_userBLL.Add(addModel))
            {
                ajax.Status = "ok";
                ajax.Msg = OperateMsgModel.AddSucMsg;
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult AdminUserEdit(int user_id = 0, string user_name = "", string user_psw = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrEmpty(user_name.Trim()))
            {
                ajax.Msg = "用户名不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(user_psw.Trim()))
            {
                ajax.Msg = "密码不能为空";
                return Json(ajax);
            }
            if (user_id == 0)
            {
                ajax.Msg = OperateMsgModel.ValidID;
                return Json(ajax);
            }
            //2.0 do
            t_admin_user editModel = OperateContext.EFBLLSession.t_admin_userBLL.GetModelBy(a=>a.ID == user_id);
            if (editModel != null)
            {
                if (OperateContext.EFBLLSession.t_admin_userBLL.GetCountBy(a => a.ID != user_id && a.user_name == user_name) > 0)
                {
                    ajax.Msg = OperateMsgModel.ExistsUserName;
                    return Json(ajax);
                }
                editModel.user_name = user_name.Trim();
                if (user_psw != "**********************")
                {
                    editModel.user_psw = SecurityHelper.GetMD5(user_psw.Trim());//user_psw;
                }
                if (OperateContext.EFBLLSession.t_admin_userBLL.Modify(editModel))
                {
                    ajax.Status = "ok";
                    ajax.Msg = OperateMsgModel.EditSucMsg;
                }
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult AdminUserDel(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_admin_userBLL.GetCountBy(a => a.ID == id) > 0)
                {
                    if (OperateContext.EFBLLSession.t_admin_userBLL.DeleteBy(a => a.ID == id))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = OperateMsgModel.DelSucMsg;
                    }
                }
            }

            return Json(ajax);
        }

        #endregion



        #region APP设置

        [HttpGet]
        public ActionResult AppSet()
        {
            t_setting service_tel_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_tel");
            ViewBag.service_tel = service_tel_model.set_value;

            t_setting wx_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_id");
            ViewBag.wx_id = wx_id_model.set_value;

            t_setting wx_public_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_public_id");
            ViewBag.wx_public_id = wx_public_id_model.set_value;

            t_setting service_qq_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_qq");
            ViewBag.service_qq = service_qq_model.set_value;

            //其他设置
            t_setting reg_coupon = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "reg_coupon");
            ViewBag.Reg_coupon = SelectHelper.GetCouponSelList(reg_coupon.set_value);

            return View();
        }

        [HttpPost]
        public ActionResult AppSet(string service_tel, string wx_id, string wx_public_id,string service_qq)
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrWhiteSpace(service_tel))
            {
                ajax.Msg = "客服电话不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrWhiteSpace(wx_id))
            {
                ajax.Msg = "微信号不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrWhiteSpace(wx_public_id))
            {
                ajax.Msg = "微信公众号不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrWhiteSpace(service_qq))
            {
                ajax.Msg = "qq号不能为空";
                return Json(ajax);
            }
            //1.0 do
            t_setting service_tel_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_tel");
            service_tel_model.set_value = service_tel;

            t_setting wx_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_id");
            wx_id_model.set_value = wx_id;

            t_setting wx_public_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_public_id");
            wx_public_id_model.set_value = wx_public_id;

            t_setting service_qq_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_qq");
            service_qq_model.set_value = service_qq;

            if (OperateContext.EFBLLSession.t_settingBLL.Modify(service_tel_model) && OperateContext.EFBLLSession.t_settingBLL.Modify(wx_id_model)
                && OperateContext.EFBLLSession.t_settingBLL.Modify(wx_public_id_model) && OperateContext.EFBLLSession.t_settingBLL.Modify(service_qq_model))
            {
                ajax.Msg = CommonBasicMsg.SaveSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.SaveFail;
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult AppOtherSet(string reg_coupon_value = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            if (!string.IsNullOrEmpty(reg_coupon_value))
            {
                t_setting reg_coupon_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "reg_coupon");
                reg_coupon_model.set_value = reg_coupon_value;
                if (OperateContext.EFBLLSession.t_settingBLL.Modify(reg_coupon_model))
                {
                    ajax.Msg = CommonBasicMsg.EditSuc;
                    ajax.Status = "ok";
                }
            }
            return Json(ajax);
        }

        #endregion



	}
}