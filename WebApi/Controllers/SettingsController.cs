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
    public class SettingsController : ApiController
    {
        /// <summary>
        /// 安卓版本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<VersionDTO> VersionInfo()
        {
            RetInfo<VersionDTO> ret = new RetInfo<VersionDTO>();
            try
            {
                VersionDTO dto = new VersionDTO();
                dto.version = 110;//1.1.0
                dto.version_lowest = 110; //1.1.0
                dto.update_content = "";
                dto.update_url = "http://oaadhsucq.bkt.clouddn.com/ZLZH.apk";
                ret.status = true;
                ret.Data = dto;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }
            return ret;
        }
    }
}
