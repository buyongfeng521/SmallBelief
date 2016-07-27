using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public class ContentHelper
    {
        /// <summary>
        /// 获得产品名称 根据ID
        /// </summary>
        /// <param name="goods_id"></param>
        /// <returns></returns>
        public static string GetGoodsName(int? goods_id = 0)
        {
            string result = "";
            t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == goods_id);
            if (goods != null)
            {
                result = goods.goods_name;
            }
            return result;
        }

        /// <summary>
        /// 获得订单状态
        /// </summary>
        /// <param name="order_status"></param>
        /// <param name="pay_status"></param>
        /// <returns></returns>
        public static string GetOrderStatusMsg(byte? order_status = 0, byte? pay_status = 0)
        {
            string result = "";
            if (order_status == 1 && pay_status == 0)
            {
                result = "待付款";
            }
            else if (order_status == 2 && pay_status == 0)
            {
                result = "已取消";
            }
            else if (order_status == 1 && pay_status == 1)
            {
                result = "配送中";
            }
            else if (order_status == 3 && pay_status == 1)
            {
                result = "待评价";
            }
            else if (order_status == 4 && pay_status == 1)
            {
                result = "完成";
            }
            return result;
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        public static string GetUserImg(int user_id = 0)
        {
            string result = "";

            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.ID == user_id);
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(user.user_img))
                {
                    result = ConfigurationHelper.AppSetting("Domain") + user.user_img;
                }
            }

            return result;
        }

        /// <summary>
        /// 获得评论的图片集合
        /// </summary>
        /// <param name="strImgs"></param>
        /// <returns></returns>
        public static string GetCommentImgs(string strImgs)
        {
            string result = "";
            if (!string.IsNullOrWhiteSpace(strImgs))
            {
                string[] strArr = strImgs.Split(',');
                if (strArr.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < strArr.Length; i++)
                    {
                        sb.Append(ConfigurationHelper.AppSetting("Domain")).Append(strArr[i]).Append(",");
                    }
                    result = sb.ToString().TrimEnd(',');
                }
            }
            return result;
        }


    }
}
