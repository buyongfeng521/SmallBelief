using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using SmallPay;
using SmallPay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class PayController : ApiController
    {
        /// <summary>
        /// 创建支付宝支付
        /// </summary>
        /// <param name="obj">{"token":"用户Token","order_id":订单ID}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<string> BuildAliPay(dynamic obj)
        {
            RetInfo<string> ret = new RetInfo<string>();

            try
            {
                string token = obj.token;
                int order_id = obj.order_id;

                t_user user = APIHelper.LoginUser(token);
                if (user != null)
                {
                    t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.user_id == user.ID && o.order_id == order_id && o.order_status == 1 && o.pay_status == 0);
                    if (order != null)
                    {
                        string order_sn = order.order_sn;
                        string subject = order.t_order_goods.FirstOrDefault().goods_name + "等";
                        decimal amount = (decimal)order.order_amount;

                        if (amount > 0)
                        {
                            AliPay pay = new AliPay();
                            string strHtmlText = pay.BuildAliPay(order.order_sn, subject, amount, EnumAliPayTradeType.APP);

                            ret.status = true;
                            ret.Data = strHtmlText;
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.OrderAmountErr;
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.PayStatusErr;
                    }
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }


            return ret;
        }

        /// <summary>
        /// 同步通知
        /// </summary>
        /// <param name="request"></param>
        [HttpGet]
        public void VerifyReturnURL(HttpRequestBase request)
        {
            Logger.WritePayLog("同步通知。。。");
            try
            {
                HttpContextBase context = (HttpContextBase)Request.Properties["MS_HttpContext"];
                request = context.Request;

                AliPay pay = new AliPay();
                AliPayReturnModel returnModel = new AliPayReturnModel();
                if (pay.VerifyReturnURL(request, out returnModel))
                {
                    if (returnModel != null)
                    {
                        APIHelper.AliPaySucProcess(returnModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExceptionLog(ex);
            }
        }

        /// <summary>
        /// 异步通知
        /// </summary>
        /// <param name="request"></param>
        [HttpPost]
        public void VerifyNotify(HttpRequestBase request)
        {
            Logger.WritePayLog("异步通知。。。。");
            try
            {
                //Request.Form["dfdfd"];
                //(HttpContextBase)Request.Properties["MS_HttpContext"];
                var c = (HttpContextBase)Request.Properties["MS_HttpContext"];//["MS_HttpContext"] as HttpRequestBase;
                request = c.Request;
                //Logger.WriteLog(request.ToString());
                AliPay pay = new AliPay();
                AliPayReturnModel returnModel = new AliPayReturnModel();
                //var c = request.Form;
                //Logger.WriteLog(request.ToString());
                //Logger.WriteLog(request.Form.ToString());
                if (pay.VerfyNotify(request, out returnModel))
                {
                    if (returnModel != null)
                    {
                        APIHelper.AliPaySucProcess(returnModel);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExceptionLog(ex);
            }
        }
    }
}
