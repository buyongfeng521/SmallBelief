using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class GoodsDTO
    {
        public int goods_id { get; set; }
        public int cat_id { get; set; }
        public int we_id { get; set; }
        public string goods_name { get; set; }
        public decimal goods_price { get; set; }
        public int goods_number { get; set; }
        //public int goods_lock_number { get; set; }
        public string goods_unit { get; set; }
        public string goods_img { get; set; }
        public string goods_brief { get; set; }
        public string goods_desc { get; set; }
        public int sort { get; set; }
        public bool is_pre_sale { get; set; }
        public bool is_hot { get; set; }
        public bool is_best { get; set; }
        public bool is_new { get; set; }
        public bool is_activity { get; set; }
        public bool is_on_sale { get; set; }
        public bool is_del { get; set; }
        public DateTime add_time { get; set; }
    }
}
