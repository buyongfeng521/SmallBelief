using Common;
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
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user_phone">手机号</param>
        /// <param name="user_psw">密码</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<UserDTO> Login(string user_phone, string user_psw)
        {
            RetInfo<UserDTO> ret = new RetInfo<UserDTO>();
            try
            {
                string strPsw = Common.SecurityHelper.GetMD5(user_psw);
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.user_phone == user_phone && u.user_psw == strPsw);
                if (user != null)
                {
                    user.last_login_time = DateTime.Now;
                    OperateContext.EFBLLSession.t_userBLL.Modify(user);

                    ret.status = true;
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
        /// 发送验证码
        /// </summary>
        /// <param name="user_phone">手机号</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<string> VCodeSend(string user_phone)
        {
            RetInfo<string> ret = new RetInfo<string>();

            try
            {
                if (!string.IsNullOrEmpty(user_phone))
                {
                    if (Common.RegHelper.IsPhone(user_phone.Trim()))
                    {
                        if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.user_phone == user_phone.Trim()) <= 0)
                        {
                            //发送短信
                            //发送短信
                            string strCode = new Random().Next(10000, 99999).ToString();
                            //"{\"code\":\"1234\",\"product\":\"窝在家\"}"
                            string strContent = string.Format("{\"code\":\"{0}\"}", strCode);
                            if (SMSHelper.SendMsgByTaoBao(user_phone, strContent, "SMS_10895108"))
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
                                    ret.Data = userCode.v_code;
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

            return ret;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user_phone">手机号</param>
        /// <param name="user_psw">密码</param>
        /// <param name="v_code">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<UserDTO> Register(string user_phone, string user_psw,string v_code)
        {
            RetInfo<UserDTO> ret = new RetInfo<UserDTO>();

            try
            {
                if (string.IsNullOrEmpty(user_phone) || string.IsNullOrEmpty(user_psw) || string.IsNullOrEmpty(v_code))
                {
                    ret.msg = "无效的注册信息";
                }
                else
                {
                    t_user_code userCode = OperateContext.EFBLLSession.t_user_codeBLL.GetModelBy(c => c.is_use == false && c.user_phone == user_phone.Trim());
                    if (userCode != null)
                    {
                        userCode.is_use = true;

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

                        if (OperateContext.EFBLLSession.t_userBLL.Add(user) && OperateContext.EFBLLSession.t_user_codeBLL.Modify(userCode))
                        {
                            ret.status = true;
                            ret.msg = "注册成功";
                            ret.Data = DTOHelper.Map<UserDTO>(user);
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
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }




    }
}
