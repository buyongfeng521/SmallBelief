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
    
        public virtual DbSet<t_user> t_user { get; set; }
        public virtual DbSet<t_admin_user> t_admin_user { get; set; }
        public virtual DbSet<t_category> t_category { get; set; }
        public virtual DbSet<t_goods> t_goods { get; set; }
    }
}
