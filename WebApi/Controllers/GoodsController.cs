using HelperCommon;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GoodsController : ApiController
    {
        [HttpGet]
        public List<t_admin_user> GoodsListGet()
        {
            List<t_admin_user> listUsers = OperateContext.EFBLLSession.t_admin_userBLL.GetListBy(u=>u.ID > 0);
            return listUsers;
        }
    }
}
