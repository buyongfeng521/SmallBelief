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

        //void
        public const string VoidPhone = "无效的手机号码";
        public const string VoidAddress = "无效的地址信息";

    }
}