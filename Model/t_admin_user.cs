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
    
    public partial class t_admin_user
    {
        public int ID { get; set; }
        public string user_name { get; set; }
        public string user_psw { get; set; }
        public Nullable<System.DateTime> last_login_time { get; set; }
        public Nullable<System.DateTime> create_time { get; set; }
    }
}
