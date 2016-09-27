using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    public class UserCouponVM
    {
        public int uc_id { get; set; }
        public int user_id { get; set; }
        public int coupon_id { get; set; }
        public bool is_use { get; set; }
        public string coupon_name { get; set; }
        public string begin_time { get; set; }
        public string end_time { get; set; }

    }
}
