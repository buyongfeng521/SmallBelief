using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class OrderStatisticsVM
    {
        public int goods_id { get; set; }
        public string goods_name { get; set; }
        public int goods_number { get; set; }
        public int order_count { get; set; }
        public int order_goods_count { get; set; }

    }
}
