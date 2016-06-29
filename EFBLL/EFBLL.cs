
 
 
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

	public partial class t_goods_galleryBLL : EFBLLBase<t_goods_gallery>
    {
		protected override void SetDAL()
        {
            dal = new t_goods_galleryDAL();
        }
    }

	public partial class t_psw_codeBLL : EFBLLBase<t_psw_code>
    {
		protected override void SetDAL()
        {
            dal = new t_psw_codeDAL();
        }
    }

	public partial class t_settingBLL : EFBLLBase<t_setting>
    {
		protected override void SetDAL()
        {
            dal = new t_settingDAL();
        }
    }

	public partial class t_userBLL : EFBLLBase<t_user>
    {
		protected override void SetDAL()
        {
            dal = new t_userDAL();
        }
    }

	public partial class t_user_codeBLL : EFBLLBase<t_user_code>
    {
		protected override void SetDAL()
        {
            dal = new t_user_codeDAL();
        }
    }


}