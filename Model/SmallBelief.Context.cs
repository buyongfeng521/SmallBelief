﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SmallBeliefEntities : DbContext
    {
        public SmallBeliefEntities()
            : base("name=SmallBeliefEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<t_ad> t_ad { get; set; }
        public virtual DbSet<t_admin_user> t_admin_user { get; set; }
        public virtual DbSet<t_area> t_area { get; set; }
        public virtual DbSet<t_banner> t_banner { get; set; }
        public virtual DbSet<t_cart> t_cart { get; set; }
        public virtual DbSet<t_category> t_category { get; set; }
        public virtual DbSet<t_category_type> t_category_type { get; set; }
        public virtual DbSet<t_city> t_city { get; set; }
        public virtual DbSet<t_comment> t_comment { get; set; }
        public virtual DbSet<t_coupon> t_coupon { get; set; }
        public virtual DbSet<t_goods> t_goods { get; set; }
        public virtual DbSet<t_goods_gallery> t_goods_gallery { get; set; }
        public virtual DbSet<t_order_goods> t_order_goods { get; set; }
        public virtual DbSet<t_order_info> t_order_info { get; set; }
        public virtual DbSet<t_province> t_province { get; set; }
        public virtual DbSet<t_psw_code> t_psw_code { get; set; }
        public virtual DbSet<t_recommend_goods> t_recommend_goods { get; set; }
        public virtual DbSet<t_room> t_room { get; set; }
        public virtual DbSet<t_sales_goods> t_sales_goods { get; set; }
        public virtual DbSet<t_setting> t_setting { get; set; }
        public virtual DbSet<t_shipping_blacklist> t_shipping_blacklist { get; set; }
        public virtual DbSet<t_user> t_user { get; set; }
        public virtual DbSet<t_user_address> t_user_address { get; set; }
        public virtual DbSet<t_user_code> t_user_code { get; set; }
        public virtual DbSet<t_user_coupon> t_user_coupon { get; set; }
        public virtual DbSet<t_wechat_seller> t_wechat_seller { get; set; }
    }
}
