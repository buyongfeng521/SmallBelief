﻿
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using EFDAL;

namespace EFBLL
{
	public partial class t_adBLL : EFBLLBase<t_ad>
    {
		protected override void SetDAL()
        {
            dal = new t_adDAL();
        }
    }

	public partial class t_admin_userBLL : EFBLLBase<t_admin_user>
    {
		protected override void SetDAL()
        {
            dal = new t_admin_userDAL();
        }
    }

	public partial class t_areaBLL : EFBLLBase<t_area>
    {
		protected override void SetDAL()
        {
            dal = new t_areaDAL();
        }
    }

	public partial class t_bannerBLL : EFBLLBase<t_banner>
    {
		protected override void SetDAL()
        {
            dal = new t_bannerDAL();
        }
    }

	public partial class t_cartBLL : EFBLLBase<t_cart>
    {
		protected override void SetDAL()
        {
            dal = new t_cartDAL();
        }
    }

	public partial class t_categoryBLL : EFBLLBase<t_category>
    {
		protected override void SetDAL()
        {
            dal = new t_categoryDAL();
        }
    }

	public partial class t_category_typeBLL : EFBLLBase<t_category_type>
    {
		protected override void SetDAL()
        {
            dal = new t_category_typeDAL();
        }
    }

	public partial class t_cityBLL : EFBLLBase<t_city>
    {
		protected override void SetDAL()
        {
            dal = new t_cityDAL();
        }
    }

	public partial class t_commentBLL : EFBLLBase<t_comment>
    {
		protected override void SetDAL()
        {
            dal = new t_commentDAL();
        }
    }

	public partial class t_couponBLL : EFBLLBase<t_coupon>
    {
		protected override void SetDAL()
        {
            dal = new t_couponDAL();
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

	public partial class t_order_goodsBLL : EFBLLBase<t_order_goods>
    {
		protected override void SetDAL()
        {
            dal = new t_order_goodsDAL();
        }
    }

	public partial class t_order_infoBLL : EFBLLBase<t_order_info>
    {
		protected override void SetDAL()
        {
            dal = new t_order_infoDAL();
        }
    }

	public partial class t_provinceBLL : EFBLLBase<t_province>
    {
		protected override void SetDAL()
        {
            dal = new t_provinceDAL();
        }
    }

	public partial class t_psw_codeBLL : EFBLLBase<t_psw_code>
    {
		protected override void SetDAL()
        {
            dal = new t_psw_codeDAL();
        }
    }

	public partial class t_recommend_goodsBLL : EFBLLBase<t_recommend_goods>
    {
		protected override void SetDAL()
        {
            dal = new t_recommend_goodsDAL();
        }
    }

	public partial class t_roomBLL : EFBLLBase<t_room>
    {
		protected override void SetDAL()
        {
            dal = new t_roomDAL();
        }
    }

	public partial class t_sales_goodsBLL : EFBLLBase<t_sales_goods>
    {
		protected override void SetDAL()
        {
            dal = new t_sales_goodsDAL();
        }
    }

	public partial class t_settingBLL : EFBLLBase<t_setting>
    {
		protected override void SetDAL()
        {
            dal = new t_settingDAL();
        }
    }

	public partial class t_shipping_blacklistBLL : EFBLLBase<t_shipping_blacklist>
    {
		protected override void SetDAL()
        {
            dal = new t_shipping_blacklistDAL();
        }
    }

	public partial class t_userBLL : EFBLLBase<t_user>
    {
		protected override void SetDAL()
        {
            dal = new t_userDAL();
        }
    }

	public partial class t_user_addressBLL : EFBLLBase<t_user_address>
    {
		protected override void SetDAL()
        {
            dal = new t_user_addressDAL();
        }
    }

	public partial class t_user_codeBLL : EFBLLBase<t_user_code>
    {
		protected override void SetDAL()
        {
            dal = new t_user_codeDAL();
        }
    }

	public partial class t_user_couponBLL : EFBLLBase<t_user_coupon>
    {
		protected override void SetDAL()
        {
            dal = new t_user_couponDAL();
        }
    }

	public partial class t_wechat_sellerBLL : EFBLLBase<t_wechat_seller>
    {
		protected override void SetDAL()
        {
            dal = new t_wechat_sellerDAL();
        }
    }


}