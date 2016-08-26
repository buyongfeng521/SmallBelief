using Common;
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
    [LoginCheck]
    public class SettingsController : Controller
    {
        [HttpGet]
        public ActionResult UserInfo()
        {
            t_admin_user user = OperateHelper.LoginUser();
            if (user == null)
            {
                return Redirect("/Home/Login");
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult UserInfo(int ID, string user_psw)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (string.IsNullOrWhiteSpace(user_psw))
            {
                ajax.Msg = "密码不能为空";
                return Json(ajax);
            }

            t_admin_user user = OperateContext.EFBLLSession.t_admin_userBLL.GetModelBy(u => u.ID == ID);
            if (user != null)
            {
                if (user_psw != "**********************")
                {
                    user.user_psw = Common.SecurityHelper.GetMD5(user_psw.Trim());
                    if (OperateContext.EFBLLSession.t_admin_userBLL.Modify(user))
                    {
                        ajax.Msg = CommonBasicMsg.EditSuc;
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.EditFail;
                    }
                }
                else
                {
                    ajax.Msg = "没有任何修改";
                }
            }
            else
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
            }

            return Json(ajax);
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

            t_setting begin_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_begin_time");
            ViewBag.Begin_Time = begin_time_model.set_value;
            t_setting end_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_end_time");
            ViewBag.End_Time = end_time_model.set_value;

            return View();
        }

        [HttpPost]
        public ActionResult AppSet(string service_tel, string wx_id, string wx_public_id, string service_qq)
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
        public ActionResult AppOtherSet(string reg_coupon_value = "", string goods_begin_time = "", string goods_end_time = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            if (!string.IsNullOrEmpty(reg_coupon_value))
            {
                t_setting reg_coupon_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "reg_coupon");
                reg_coupon_model.set_value = reg_coupon_value;

                t_setting begin_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_begin_time");
                begin_time_model.set_value = goods_begin_time;
                t_setting end_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_end_time");
                end_time_model.set_value = goods_end_time;

                if (OperateContext.EFBLLSession.t_settingBLL.Modify(reg_coupon_model) 
                    && OperateContext.EFBLLSession.t_settingBLL.Modify(begin_time_model)
                    && OperateContext.EFBLLSession.t_settingBLL.Modify(end_time_model))
                {
                    ajax.Msg = CommonBasicMsg.EditSuc;
                    ajax.Status = "ok";
                }
            }
            return Json(ajax);
        }

        #endregion



        [HttpGet]
        public ActionResult Blacklist()
        {
            List<t_shipping_blacklist> shipping = OperateContext.EFBLLSession.t_shipping_blacklistBLL.GetListBy(s=>s.sb_id > 0);

            //List<t_room> listRoom = OperateContext.EFBLLSession.t_roomBLL.GetListBy();

            string sqlArea = "select distinct area from t_room  order by Area";
            List<string> listArea = DapperContext<t_room>.DapperBLL.QueryListSql(sqlArea, null).Select(r => r.area).ToList();
            SelectList selArea = new SelectList(listArea);

            string sqlBuilding = "select distinct building from t_room where area = @area order by building";
            List<string> listBuilding = DapperContext<t_room>.DapperBLL.QueryListSql(sqlBuilding, new { area = listArea[0] }).Select(r => r.building).ToList();
            SelectList selBuilding = new SelectList(listBuilding);

            string sqlFloor = "select distinct floor from t_room where area = @area and building = @building order by floor";
            List<string> listFloor = DapperContext<t_room>.DapperBLL.QueryListSql(sqlFloor, new { area = listArea[0], building = listBuilding[0] }).Select(r => r.floor).ToList();
            SelectList selFloor = new SelectList(listFloor);


            ViewBag.SelArea = selArea;
            ViewBag.SelBuilding = selBuilding;
            ViewBag.SelFloor = selFloor;


            return View(shipping);
        }

        [HttpPost]
        public ActionResult BlacklistAdd(string ddlArea, string ddlBuilding, string ddlFloor)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (string.IsNullOrEmpty(ddlArea))
            {
                ajax.Msg = "区域不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(ddlBuilding))
            {
                ajax.Msg = "楼号不能为空";
                return Json(ajax);
            }
            if (string.IsNullOrEmpty(ddlFloor))
            {
                ajax.Msg = "楼层不能为空";
                return Json(ajax);
            }

            if (OperateContext.EFBLLSession.t_shipping_blacklistBLL.GetCountBy(s => s.area == ddlArea && s.building == ddlBuilding && s.floor == ddlFloor) <= 0)
            {
                t_shipping_blacklist addModel = new t_shipping_blacklist() 
                {
                    area = ddlArea,
                    building = ddlBuilding,
                    floor = ddlFloor,
                    add_time = DateTime.Now
                };

                if (OperateContext.EFBLLSession.t_shipping_blacklistBLL.Add(addModel))
                {
                    ajax.Msg = "成功";
                    ajax.Status = "ok";
                }
                else
                {
                    ajax.Msg = "失败";
                }
            }
            else
            {
                ajax.Msg = "已存在";
            }

            return Json(ajax);
        }

        


        [HttpPost]
        public ActionResult BlacklistDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_shipping_blacklistBLL.DeleteBy(d => d.sb_id == id))
                {
                    ajax.Msg = CommonBasicMsg.DelSuc;
                    ajax.Status = "ok";
                }
            }

            return Json(ajax);
        }


        [HttpGet]
        public ActionResult BlacklistBuildingGet(string area)
        {
            string sqlBuilding = "select distinct building from t_room where area = @area order by building";
            List<string> listBuilding = DapperContext<t_room>.DapperBLL.QueryListSql(sqlBuilding, new { area = area }).Select(r => r.building).ToList();
            //SelectList selBuilding = new SelectList(listBuilding);
            return Json(listBuilding, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BlacklistFloorGet(string area, string building)
        {
            string sqlFloor = "select distinct floor from t_room where area = @area and building = @building order by floor";
            List<string> listFloor = DapperContext<t_room>.DapperBLL.QueryListSql(sqlFloor, new { area = area, building = building }).Select(r => r.floor).ToList();
            //SelectList selFloor = new SelectList(listFloor);

            return Json(listFloor, JsonRequestBehavior.AllowGet);
        }



	}
}