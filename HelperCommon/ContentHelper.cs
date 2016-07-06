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


    }
}
