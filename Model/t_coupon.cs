//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_coupon
    {
        public int coupon_id { get; set; }
        public Nullable<int> coupon_type { get; set; }
        public string coupon_name { get; set; }
        public string coupon_img { get; set; }
        public Nullable<decimal> condition_amount { get; set; }
        public Nullable<decimal> coupon_amount { get; set; }
        public Nullable<int> valid_days { get; set; }
        public Nullable<bool> is_del { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
    }
}
