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
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CommonController : ApiController
    {

        /// <summary>
        /// 在线客服信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<OnlineDTO> OnlineServiceGet()
        {
            RetInfo<OnlineDTO> ret = new RetInfo<OnlineDTO>();

            try
            {
                OnlineDTO dtoModel = new OnlineDTO();

                t_setting service_tel_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_tel");
                t_setting wx_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_id");
                t_setting wx_public_id_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "wx_public_id");
                t_setting service_qq_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "service_qq");

                dtoModel.service_tel = service_tel_model.set_value;
                dtoModel.wx_id = wx_id_model.set_value;
                dtoModel.wx_public_id = wx_public_id_model.set_value;
                dtoModel.service_qq = service_qq_model.set_value;

                ret.Data = dtoModel;
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }


        /// <summary>
        /// 获得七牛ToKen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<string> QiNiuTokenGet()
        {
            RetInfo<string> ret = new RetInfo<string>();

            try
            {
                ret.Data = QiNiuHelper.GetToken();
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 默认地址是否在黑名单
        /// </summary>
        /// <param name="token">用户token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<object> IsAdressBlack(string token)
        {
            RetInfo<object> ret = new RetInfo<object>();
            ret.Data = false;
            try
            {
                t_user user = APIHelper.LoginUser(token);
                if (user != null)
                {
                    t_user_address userAddress = OperateContext.EFBLLSession.t_user_addressBLL.GetModelBy(a => a.user_id == user.ID && a.is_default == true);
                    if (userAddress != null)
                    {
                        string floor = ContentHelper.GetFloorByRoom(userAddress.room_num);
                        if (OperateContext.EFBLLSession.t_shipping_blacklistBLL.GetCountBy(s => s.area == userAddress.area && s.building == userAddress.building && s.floor == floor) > 0)
                        {
                            ret.status = true;
                            ret.Data = true;
                        }
                    }
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExceptionLog(ex);
                ret.msg = ex.ToString();
            }

            return ret;
        }

        
    }
}
