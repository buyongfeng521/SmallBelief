
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
    
public partial class t_category
{

    public t_category()
    {

        this.t_goods = new HashSet<t_goods>();

    }


    public int cat_id { get; set; }

    public Nullable<byte> cat_type { get; set; }

    public string cat_name { get; set; }

    public string cat_img { get; set; }

    public string cat_note { get; set; }

    public Nullable<int> sort { get; set; }

    public Nullable<System.DateTime> add_time { get; set; }



    public virtual ICollection<t_goods> t_goods { get; set; }

}

}
