using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    /// <summary>
    /// 通用Msg
    /// </summary>
    public class Message
    {
        
        public const string Suc = "ok";
        public const string NullData = "数据为空";
        public const string NoLogin = "用户未登录";
        public const string NoAccess = "用户无权限";

        //void
        public const string VoidPhone = "无效的手机号码";
        public const string VoidAddress = "无效的地址信息";


        //order
        public const string OrderCreateSuc = "订单生成成功";
        public const string OrderCreateFail = "订单生成失败";


    }
}