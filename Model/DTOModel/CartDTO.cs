using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class CartDTO
    {
        public int cart_id { get; set; }
        public byte cart_type { get; set; }
        public int user_id { get; set; }
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public string goods_img { get; set; }
        public decimal market_price { get; set; }
        public decimal goods_price { get; set; }
        public int goods_number { get; set; }
        public int repertory_number { get; set; }
        public bool is_activity { get; set; }

    }
}
