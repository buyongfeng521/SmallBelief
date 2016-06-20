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
using System.Web.Http;

namespace WebApi.Controllers
{
    public class LoginController : ApiController
    {

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user_phone"></param>
        /// <param name="user_psw"></param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<UserDTO> Login(string user_phone, string user_psw)
        {
            RetInfo<UserDTO> ret = new RetInfo<UserDTO>();
            try
            {
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.user_phone == user_phone && u.user_psw == user_psw);
                if (user != null)
                {
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




    }
}
