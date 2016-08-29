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
                    OperateContext.EFBLLSession.t_order_infoBLL.Modify(order);
                    //更新数据
                    List<t_order_goods> listGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o=>o.order_id == order.order_id);
                    listGoods.ForEach(data => {
                        t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g=>g.goods_id == data.goods_id);
                        if (goods != null)
                        {
                            goods.goods_lock_number = (goods.goods_lock_number - data.goods_number) < 0 ? 0 : (goods.goods_lock_number - data.goods_number);
                            goods.goods_number = (goods.goods_number - data.goods_number) < 0 ? 0 : (goods.goods_number - data.goods_number);
                            OperateContext.EFBLLSession.t_goodsBLL.Modify(goods);
                        }
                    });
                }
                
            }
        }


        public static bool OrderCancel(int order_id = 0)
        {
            bool result = false;

            t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_id == order_id && o.order_status == 1 && o.pay_status == 0);
            if (order != null)
            {
                //1.0 锁定库存
                List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == order.order_id);
                listOrderGoods.ForEach(item =>
                {
                    t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == item.goods_id);
                    if (goods != null)
                    {
                        goods.goods_lock_number = (goods.goods_lock_number - item.goods_number) >= 0 ? (goods.goods_lock_number - item.goods_number) : 0;
                        OperateContext.EFBLLSession.t_goodsBLL.Modify(goods);
                    }
                });
                //2.0 订单状态
                order.order_status = 2;
                order.cancel_time = DateTime.Now;
                OperateContext.EFBLLSession.t_order_infoBLL.Modify(order);
                //3.0 优惠券
                if (order.uc_id > 0)
                {
                    t_user_coupon userCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetModelBy(u => u.uc_id == order.uc_id);
                    userCoupon.is_use = false;
                    userCoupon.use_time = null;
                    OperateContext.EFBLLSession.t_user_couponBLL.Modify(userCoupon);
                }

                result = true;
            }

            return result;
        }


        /// <summary>
        /// 是否黑名单
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool IsBlacklist(string token = "")
        {
            bool result = false;

            if (!string.IsNullOrEmpty(token))
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
                            result = true;
                        }
                    }
                }
            }

            return result;
        }


    }
}
