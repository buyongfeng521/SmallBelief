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
    public class CommonController : ApiController
    {
        
        /// <summary>
        /// 错误信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<object> Error()
        {
            RetInfo<object> ret = new RetInfo<object>();
            try
            {
                ret.msg = Message.NoLogin;
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
