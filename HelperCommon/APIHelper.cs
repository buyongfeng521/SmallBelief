using Common;
using Model;
using SmallPay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public class APIHelper
    {
        public static bool IsLogin(string token)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(token))
            {
                if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.token == token) > 0)
                {
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// 登录用户
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static t_user LoginUser(string token)
        {
            t_user user = null;

            if (!string.IsNullOrWhiteSpace(token))
            {
                user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
            }

            return user;
        }


        /// <summary>
        /// 支付宝支付成功数据处理
        /// </summary>
        /// <param name="returnModel"></param>
        public static void AliPaySucProcess(AliPayReturnModel returnModel)
        {
            if (returnModel.TradeStatus == "TRADE_FINISHED" || returnModel.TradeStatus == "TRADE_SUCCESS")
            {
                StringBuilder sBuild = new StringBuilder();
                sBuild.Append("商户订单号：").Append(returnModel.OutTradeNo).Append(";")
                    .Append("交易订单号：").Append(returnModel.TradeNo).Append(";")
                    .Append("交易总金额：").Append(returnModel.TotalFee).Append(";")
                    .Append("交易状态：").Append(returnModel.TradeStatus).Append(";")
                    .Append("交易时间：").Append(DateTime.Now.ToString()).Append(";");
                Logger.WritePayLog(sBuild.ToString());

                t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_sn == returnModel.OutTradeNo && o.order_status == 1 && o.pay_status == 0);
                if (order != null)
                {
                    order.money_paid = returnModel.TotalFee;
                    order.pay_status = 1;
                    order.pay_time = DateTime.Now;
                }
                OperateContext.EFBLLSession.t_order_infoBLL.Modify(order);
            }
        }


    }
}
