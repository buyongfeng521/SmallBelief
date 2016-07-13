using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonModel
{
    public class Enums
    {
        public enum CategoryType
        { 
            [Description("水果")]
            水果 = 0,
            [Description("零用品")]
            零用品 = 1,
            [Description("日用品")]
            日用品 = 2,
            [Description("微商")]
            微商 = 3,
            [Description("其他")]
            其他 = 4
        }


        //Banner类型
        public enum BannerType
        {
            [Description("首页banner")]
            首页Banner = 0
            //[Description("美购Banner")]
            //美购Banner = 1
        }

        //广告类型
        public enum ADType
        { 
            [Description("首页广告")]
            首页广告 = 0
        }

        //ClickType
        public enum ClickType
        {
            //点击类型(0:不可点击,1:分类2:产品,3:URL)
            [Description("不可点击")]
            不可点击 = 0,
            [Description("分类")]
            分类 = 1,
            [Description("产品")]
            产品=2,
            [Description("URL")]
            URL = 3
        }


        //购物车类型
        public enum CartType
        { 
            [Description("普通")]
            普通 = 0,
            [Description("预购")]
            预购 = 1
        }

        //订单类型
        public enum OrderType
        {
            [Description("普通")]
            普通 = 0,
            [Description("预购")]
            预购 = 1
        }





    }
}
