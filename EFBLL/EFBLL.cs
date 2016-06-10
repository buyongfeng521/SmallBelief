﻿
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using EFDAL;

namespace EFBLL
{
	public partial class t_admin_userBLL : EFBLLBase<t_admin_user>
    {
		protected override void SetDAL()
        {
            dal = new t_admin_userDAL();
        }
    }

	public partial class t_categoryBLL : EFBLLBase<t_category>
    {
		protected override void SetDAL()
        {
            dal = new t_categoryDAL();
        }
    }

	public partial class t_goodsBLL : EFBLLBase<t_goods>
    {
		protected override void SetDAL()
        {
            dal = new t_goodsDAL();
        }
    }

	public partial class t_userBLL : EFBLLBase<t_user>
    {
		protected override void SetDAL()
        {
            dal = new t_userDAL();
        }
    }


}