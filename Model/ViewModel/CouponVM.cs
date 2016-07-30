using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class CouponVM
    {
        public int coupon_id { get; set; }
        public string coupon_name { get; set; }
        public string coupon_img { get; set; }
        public decimal condition_amount { get; set; }
        public decimal coupon_amount { get; set; }
        public int valid_days { get; set; }
        //public bool is_del { get; set; }
        //public datetime create_time { get; set; }
    }
}
