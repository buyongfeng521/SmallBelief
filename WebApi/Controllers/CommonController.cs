using HelperCommon;
using Model;
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

                dtoModel.service_tel = service_tel_model.set_value;
                dtoModel.wx_id = wx_id_model.set_value;
                dtoModel.wx_public_id = wx_public_id_model.set_value;

                ret.Data = dtoModel;
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        
    }
}
