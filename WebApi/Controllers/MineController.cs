using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [CustomAuthorize]
    public class MineController : ApiController
    {
        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="nickname">昵称</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserNickNameModify(string token, string nickname)
        { 
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.ID != user.ID && u.user_name == nickname.Trim()) <= 0)
                        {
                            user.user_name = nickname.Trim();
                            if (OperateContext.EFBLLSession.t_userBLL.Modify(user))
                            {
                                ret.msg = CommonBasicMsg.EditSuc;
                                ret.status = true;
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.EditFail;
                            }
                        }
                        else
                        {
                            ret.msg = "已存在此昵称";
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = "昵称不能为空";
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="old_psw">旧密码</param>
        /// <param name="new_psw">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserPswModify(string token, string old_psw, string new_psw)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {

                if (old_psw != null && !string.IsNullOrEmpty(old_psw.Trim()) && new_psw != null && !string.IsNullOrEmpty(new_psw.Trim()))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        if (user.user_psw == SecurityHelper.GetMD5(old_psw.Trim()))
                        {
                            user.user_psw = SecurityHelper.GetMD5(new_psw.Trim());
                            if (OperateContext.EFBLLSession.t_userBLL.Modify(user))
                            {
                                ret.msg = CommonBasicMsg.EditSuc;
                                ret.status = true;
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.EditFail;
                            }
                        }
                        else
                        {
                            ret.msg = "旧密码错误";
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = "请输入有效的密码";
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
