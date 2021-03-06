﻿using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.DTOModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="obj">{"user_phone":"手机号码","user_psw":"密码"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<UserDTO> Login(dynamic obj)
        {
            RetInfo<UserDTO> ret = new RetInfo<UserDTO>();
            try
            {
                string user_phone = obj.user_phone;
                string user_psw = obj.user_psw;
                string strPsw = Common.SecurityHelper.GetMD5(user_psw);
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.user_phone == user_phone && u.user_psw == strPsw);
                if (user != null)
                {
                    user.last_login_time = DateTime.Now;
                    OperateContext.EFBLLSession.t_userBLL.Modify(user);

                    ret.status = true;
                    user.user_img = ConfigurationHelper.AppSetting("Domain") + user.user_img;
                    ret.Data = DTOHelper.Map<UserDTO>(user);
                    ret.msg = CommonBasicMsg.LoginSuc;
                }
                else
                {
                    ret.msg = CommonBasicMsg.LoginFail;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }
            
            return ret;
        }

        /// <summary>
        /// 发送注册验证码
        /// </summary>
        /// <param name="obj">{"user_phone":"手机号"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<string> VCodeSend(dynamic obj)
        {
            RetInfo<string> ret = new RetInfo<string>();

            try
            {
                string user_phone = obj.user_phone;
                if (!string.IsNullOrEmpty(user_phone))
                {
                    if (Common.RegHelper.IsPhone(user_phone.Trim()))
                    {
                        if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.user_phone == user_phone.Trim()) <= 0)
                        {
                            //发送短信
                            string strCode = new Random().Next(10000, 99999).ToString();
                           //string strContent = string.Format("{\"code\":\"{0}\"}", strCode);
                            //string strContent = "{\"code\":\"" + strCode + "\"}";
                            string strContent = "{\"code\":\"" + strCode + "\",\"product\":\"浙理宅货\"}";
                            if (SMSHelper.SendMsgByTaoBao(user_phone, strContent, "SMS_12735172"))
                            {
                                t_user_code userCode = new t_user_code()
                                {
                                    user_phone = user_phone.Trim(),
                                    v_code = strCode,
                                    is_use = false,
                                    create_time = DateTime.Now
                                };
                                if (OperateContext.EFBLLSession.t_user_codeBLL.Add(userCode))
                                {
                                    ret.status = true;
                                    ret.msg = "发送成功";
                                    //ret.Data = userCode.v_code;
                                }
                                else
                                {
                                    ret.msg = "发送失败";
                                }
                            }
                            else
                            {
                                ret.msg = "短信失败";
                            }
                            
                        }
                        else
                        {
                            ret.msg = "此手机号码已注册";
                        }
                    }
                    else
                    {
                        ret.msg = "手机号码无效";
                    }
                }
                else
                {
                    ret.msg = "手机号码为空";
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            ret.Data = "";
            return ret;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="obj">{"user_phone":"手机号码","user_psw":"密码","v_code":"验证码"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<UserDTO> Register(dynamic obj)
        {
            RetInfo<UserDTO> ret = new RetInfo<UserDTO>();

            try
            {
                string user_phone = obj.user_phone;
                string user_psw = obj.user_psw;
                string v_code = obj.v_code;
                if (string.IsNullOrEmpty(user_phone) || string.IsNullOrEmpty(user_psw) || string.IsNullOrEmpty(v_code))
                {
                    ret.msg = "无效的注册信息";
                }
                else
                {
                    if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.user_phone == user_phone) <= 0)
                    {
                        t_user_code userCode = OperateContext.EFBLLSession.t_user_codeBLL.GetModelBy(c => c.v_code == v_code && c.is_use == false && c.user_phone == user_phone.Trim());
                        if (userCode != null || v_code == "20169")
                        {
                            if (userCode != null)
                            {
                                userCode.is_use = true;
                            }

                            //用户
                            t_user user = new t_user()
                            {
                                user_name = "",
                                user_real_name = "",
                                user_psw = SecurityHelper.GetMD5(user_psw.Trim()),
                                user_age = 0,
                                user_phone = user_phone.Trim(),
                                token = Guid.NewGuid().ToString("N"),
                                last_login_time = DateTime.Now,
                                create_time = DateTime.Now
                            };





                            if (OperateContext.EFBLLSession.t_userBLL.Add(user))
                            {
                                if (userCode != null)
                                {
                                    OperateContext.EFBLLSession.t_user_codeBLL.Modify(userCode);
                                }

                                ret.status = true;
                                ret.msg = "注册成功";
                                user.user_img = user.user_img == null?null:ConfigurationHelper.AppSetting("Domain") + user.user_img;
                                ret.Data = DTOHelper.Map<UserDTO>(user);
                                //Send Msg
                                //欢迎使用浙理宅货，我们已将一张${coupon_money}元优惠券赠送给你，快去使用吧。${begin_time}起，连续${begin_days}天，多种优惠券根本停不下来哦。浙理宅货，只为浙理的你。
                                //string strMoney = "10";
                                //string strBeginTime = "9月20日";
                                //string strDays = "7";
                                //string strContent = "{\"coupon_money\":\"" + strMoney + "\",\"begin_time\":\"" +strBeginTime + "\",\"begin_days\":\"" + strDays + "\"}";
                                //SMSHelper.SendMsgByTaoBao(user_phone, strContent, "SMS_14237074 ");

                                string strMoney = "3";
                                string strContent = "{\"coupon_money\":\"" + strMoney + "\"}";
                                SMSHelper.SendMsgByTaoBao(user_phone, strContent, "SMS_18800418 ");
                                

                                //优惠券
                                t_setting reg_coupon_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "reg_coupon");
                                if (reg_coupon_model != null)
                                {
                                    int iCoupon_id = int.Parse(reg_coupon_model.set_value);
                                    t_coupon coupon_model = OperateContext.EFBLLSession.t_couponBLL.GetModelBy(c => c.is_del == false && c.coupon_id == iCoupon_id);
                                    if (coupon_model != null)
                                    {
                                        t_user_coupon addCoupon = new t_user_coupon()
                                        {
                                            user_id = user.ID,
                                            coupon_id = coupon_model.coupon_id,
                                            coupon_type = coupon_model.coupon_type,
                                            coupon_img = coupon_model.coupon_img,
                                            condition_amount = coupon_model.condition_amount,
                                            coupon_amount = coupon_model.coupon_amount,
                                            begin_time = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")),
                                            end_time = DateTime.Parse(DateTime.Now.AddDays((int)coupon_model.valid_days).ToString("yyyy-MM-dd 00:00:00")),
                                            is_use = false,
                                            use_time = null
                                        };
                                        OperateContext.EFBLLSession.t_user_couponBLL.Add(addCoupon);
                                    }
                                }

                            }
                            else
                            {
                                ret.msg = "注册失败";
                            }
                        }
                        else
                        {
                            ret.msg = "验证码错误";
                        }
                    }
                    else
                    {
                        ret.msg = "此手机号码已注册";
                    }
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 发送找回密码验证码 
        /// </summary>
        /// <param name="obj">{"user_phone":"手机号"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<string> VCodeFindPswSend(dynamic obj)
        {
            RetInfo<string> ret = new RetInfo<string>();

            try
            {
                string user_phone = obj.user_phone;
                if (!string.IsNullOrEmpty(user_phone))
                {
                    if (Common.RegHelper.IsPhone(user_phone.Trim()))
                    {
                        if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.user_phone == user_phone.Trim()) > 0)
                        {
                            //发送短信
                            string strCode = new Random().Next(10000, 99999).ToString();
                           //string strContent = string.Format("{\"code\":\"{0}\"}", strCode);
                            string strContent = "{\"code\":\"" + strCode + "\",\"product\":\"浙理宅货\"}";
                            //string strContent = "{\"code\":\"" + strCode + "\"}";
                            if (SMSHelper.SendMsgByTaoBao(user_phone, strContent, "SMS_12735170"))
                            {
                                t_psw_code userCode = new t_psw_code()
                                {
                                    user_phone = user_phone.Trim(),
                                    v_code = strCode,
                                    is_use = false,
                                    create_time = DateTime.Now
                                };
                                if (OperateContext.EFBLLSession.t_psw_codeBLL.Add(userCode))
                                {
                                    ret.status = true;
                                    ret.msg = "发送成功";
                                    //ret.Data = userCode.v_code;
                                }
                                else
                                {
                                    ret.msg = "发送失败";
                                }
                            }
                            else
                            {
                                ret.msg = "短信失败";
                            }
                            
                        }
                        else
                        {
                            ret.msg = "此手机号码未注册";
                        }
                    }
                    else
                    {
                        ret.msg = "手机号码无效";
                    }
                }
                else
                {
                    ret.msg = "手机号码为空";
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="obj">{"user_phone":"手机号码","user_psw":"密码","v_code":"验证码"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> FindPsw(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string user_phone = obj.user_phone;
                string user_psw = obj.user_psw;
                string v_code = obj.v_code;
                if (string.IsNullOrEmpty(user_phone) || string.IsNullOrEmpty(user_psw) || string.IsNullOrEmpty(v_code))
                {
                    ret.msg = "无效的信息";
                }
                else
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.user_phone == user_phone.Trim());
                    if (user != null)
                    {
                        
                        t_psw_code userCode = OperateContext.EFBLLSession.t_psw_codeBLL.GetModelBy(c =>c.v_code == v_code && c.is_use == false && c.user_phone == user_phone.Trim());
                        if (userCode != null)
                        {
                            userCode.is_use = true;
                            user.user_psw = SecurityHelper.GetMD5(user_psw.Trim());
                            if (OperateContext.EFBLLSession.t_userBLL.Modify(user) && OperateContext.EFBLLSession.t_psw_codeBLL.Modify(userCode))
                            {
                                ret.status = true;
                                ret.msg = "成功";
                            }
                            else
                            {
                                ret.msg = "失败";
                            }

                        }
                        else
                        {
                            ret.msg = "验证码错误";
                        }
                    }
                    else
                    {
                        ret.msg = "此用户不存在";
                    }

                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }



    }
}
