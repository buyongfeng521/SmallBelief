using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class OrderInfoDTO
    {
        public int order_id { get; set; }
        public byte order_type { get; set; }
        public string order_sn { get; set; }
        public int user_id { get; set; }
        public byte order_status { get; set; }
        public byte shipping_status { get; set; }
        public byte pay_status { get; set; }
        public string consignee { get; set; }
        public string mobile { get; set; }
        public string area { get; set; }
        public string building { get; set; }
        public string room_num { get; set; }
        public string address { get; set; }
        public string postscript { get; set; }
        public decimal goods_amount { get; set; }
        public decimal order_amount { get; set; }
        public decimal money_paid { get; set; }
        public string add_time { get; set; }
        public string confirm_time { get; set; }
        public string pay_time { get; set; }
        public string shipping_time { get; set; }
    }

}
