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
    
    public partial class t_goods
    {
        public t_goods()
        {
            this.t_cart = new HashSet<t_cart>();
            this.t_goods_gallery = new HashSet<t_goods_gallery>();
            this.t_recommend_goods = new HashSet<t_recommend_goods>();
            this.t_sales_goods = new HashSet<t_sales_goods>();
        }
    
        public int goods_id { get; set; }
        public Nullable<int> cat_id { get; set; }
        public Nullable<int> we_id { get; set; }
        public string goods_name { get; set; }
        public Nullable<decimal> goods_price { get; set; }
        public Nullable<int> goods_number { get; set; }
        public Nullable<int> goods_lock_number { get; set; }
        public string goods_unit { get; set; }
        public string goods_img { get; set; }
        public string goods_brief { get; set; }
        public string goods_brief2 { get; set; }
        public string goods_desc { get; set; }
        public Nullable<int> sort { get; set; }
        public Nullable<bool> is_pre_sale { get; set; }
        public Nullable<bool> is_hot { get; set; }
        public Nullable<bool> is_best { get; set; }
        public Nullable<bool> is_new { get; set; }
        public Nullable<bool> is_activity { get; set; }
        public Nullable<bool> is_on_sale { get; set; }
        public Nullable<bool> is_del { get; set; }
        public Nullable<System.DateTime> add_time { get; set; }
        public Nullable<int> goods_multiple { get; set; }
    
        public virtual ICollection<t_cart> t_cart { get; set; }
        public virtual t_category t_category { get; set; }
        public virtual ICollection<t_goods_gallery> t_goods_gallery { get; set; }
        public virtual ICollection<t_recommend_goods> t_recommend_goods { get; set; }
        public virtual ICollection<t_sales_goods> t_sales_goods { get; set; }
    }
}
