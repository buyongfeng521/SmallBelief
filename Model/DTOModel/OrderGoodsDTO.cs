using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class OrderGoodsDTO
    {
        public int rec_id { get; set; }
        //public int order_id { get; set; }
        //public int goods_id { get; set; }
        public string goods_name { get; set; }
        public int goods_number { get; set; }
        //public decimal market_price { get; set; }
        public decimal goods_price { get; set; }

    }
}
