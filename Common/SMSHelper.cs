using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Common
{
    public class SMSHelper
    {
        /// <summary>
        /// 阿里大鱼短信服务
        /// </summary>
        /// <param name="s_mobile">手机号码</param>
        /// <param name="s_content">Json替换变量</param>
        /// <param name="temp_code">短信模板ID</param>
        /// <returns></returns>
        public static bool SendMsgByTaoBao(string s_mobile, string s_content, string temp_code)
        {
            try
            {
                ITopClient client = new DefaultTopClient("http://gw.api.taobao.com/router/rest", ConfigurationHelper.AppSetting("SMSAppKey"), ConfigurationHelper.AppSetting("SMSAppSecret"));
                AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
                req.Extend = "123456";
                req.SmsType = "normal";
                req.SmsFreeSignName = ConfigurationHelper.AppSetting("SMSSignName");
                //验证码${code}，您正在注册成为${product}用户，感谢您的支持！ 
                req.SmsParam = s_content;//"{\"code\":\"1234\",\"product\":\"窝在家\"}";
                req.RecNum = s_mobile;//"18601681037";
                req.SmsTemplateCode = temp_code;//"SMS_6750040";
                AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
                //Console.WriteLine(rsp.Body);
            }
            catch
            {

            }
            return true;
        }
    }
}
