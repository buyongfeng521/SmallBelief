
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFBLL
{
    public class EFBLLSession
    {
        
        private t_adBLL t_adbll;
        public t_adBLL t_adBLL
        {
	        get
	        {
		        if(t_adbll == null)
		        {
			        t_adbll = new t_adBLL();
		        }
		        return t_adbll;
	        }
	        set{}
        }

        
        private t_admin_userBLL t_admin_userbll;
        public t_admin_userBLL t_admin_userBLL
        {
	        get
	        {
		        if(t_admin_userbll == null)
		        {
			        t_admin_userbll = new t_admin_userBLL();
		        }
		        return t_admin_userbll;
	        }
	        set{}
        }

        
        private t_areaBLL t_areabll;
        public t_areaBLL t_areaBLL
        {
	        get
	        {
		        if(t_areabll == null)
		        {
			        t_areabll = new t_areaBLL();
		        }
		        return t_areabll;
	        }
	        set{}
        }

        
        private t_bannerBLL t_bannerbll;
        public t_bannerBLL t_bannerBLL
        {
	        get
	        {
		        if(t_bannerbll == null)
		        {
			        t_bannerbll = new t_bannerBLL();
		        }
		        return t_bannerbll;
	        }
	        set{}
        }

        
        private t_cartBLL t_cartbll;
        public t_cartBLL t_cartBLL
        {
	        get
	        {
		        if(t_cartbll == null)
		        {
			        t_cartbll = new t_cartBLL();
		        }
		        return t_cartbll;
	        }
	        set{}
        }

        
        private t_categoryBLL t_categorybll;
        public t_categoryBLL t_categoryBLL
        {
	        get
	        {
		        if(t_categorybll == null)
		        {
			        t_categorybll = new t_categoryBLL();
		        }
		        return t_categorybll;
	        }
	        set{}
        }

        
        private t_category_typeBLL t_category_typebll;
        public t_category_typeBLL t_category_typeBLL
        {
	        get
	        {
		        if(t_category_typebll == null)
		        {
			        t_category_typebll = new t_category_typeBLL();
		        }
		        return t_category_typebll;
	        }
	        set{}
        }

        
        private t_cityBLL t_citybll;
        public t_cityBLL t_cityBLL
        {
	        get
	        {
		        if(t_citybll == null)
		        {
			        t_citybll = new t_cityBLL();
		        }
		        return t_citybll;
	        }
	        set{}
        }

        
        private t_commentBLL t_commentbll;
        public t_commentBLL t_commentBLL
        {
	        get
	        {
		        if(t_commentbll == null)
		        {
			        t_commentbll = new t_commentBLL();
		        }
		        return t_commentbll;
	        }
	        set{}
        }

        
        private t_couponBLL t_couponbll;
        public t_couponBLL t_couponBLL
        {
	        get
	        {
		        if(t_couponbll == null)
		        {
			        t_couponbll = new t_couponBLL();
		        }
		        return t_couponbll;
	        }
	        set{}
        }

        
        private t_goodsBLL t_goodsbll;
        public t_goodsBLL t_goodsBLL
        {
	        get
	        {
		        if(t_goodsbll == null)
		        {
			        t_goodsbll = new t_goodsBLL();
		        }
		        return t_goodsbll;
	        }
	        set{}
        }

        
        private t_goods_galleryBLL t_goods_gallerybll;
        public t_goods_galleryBLL t_goods_galleryBLL
        {
	        get
	        {
		        if(t_goods_gallerybll == null)
		        {
			        t_goods_gallerybll = new t_goods_galleryBLL();
		        }
		        return t_goods_gallerybll;
	        }
	        set{}
        }

        
        private t_order_goodsBLL t_order_goodsbll;
        public t_order_goodsBLL t_order_goodsBLL
        {
	        get
	        {
		        if(t_order_goodsbll == null)
		        {
			        t_order_goodsbll = new t_order_goodsBLL();
		        }
		        return t_order_goodsbll;
	        }
	        set{}
        }

        
        private t_order_infoBLL t_order_infobll;
        public t_order_infoBLL t_order_infoBLL
        {
	        get
	        {
		        if(t_order_infobll == null)
		        {
			        t_order_infobll = new t_order_infoBLL();
		        }
		        return t_order_infobll;
	        }
	        set{}
        }

        
        private t_provinceBLL t_provincebll;
        public t_provinceBLL t_provinceBLL
        {
	        get
	        {
		        if(t_provincebll == null)
		        {
			        t_provincebll = new t_provinceBLL();
		        }
		        return t_provincebll;
	        }
	        set{}
        }

        
        private t_psw_codeBLL t_psw_codebll;
        public t_psw_codeBLL t_psw_codeBLL
        {
	        get
	        {
		        if(t_psw_codebll == null)
		        {
			        t_psw_codebll = new t_psw_codeBLL();
		        }
		        return t_psw_codebll;
	        }
	        set{}
        }

        
        private t_roomBLL t_roombll;
        public t_roomBLL t_roomBLL
        {
	        get
	        {
		        if(t_roombll == null)
		        {
			        t_roombll = new t_roomBLL();
		        }
		        return t_roombll;
	        }
	        set{}
        }

        
        private t_settingBLL t_settingbll;
        public t_settingBLL t_settingBLL
        {
	        get
	        {
		        if(t_settingbll == null)
		        {
			        t_settingbll = new t_settingBLL();
		        }
		        return t_settingbll;
	        }
	        set{}
        }

        
        private t_userBLL t_userbll;
        public t_userBLL t_userBLL
        {
	        get
	        {
		        if(t_userbll == null)
		        {
			        t_userbll = new t_userBLL();
		        }
		        return t_userbll;
	        }
	        set{}
        }

        
        private t_user_addressBLL t_user_addressbll;
        public t_user_addressBLL t_user_addressBLL
        {
	        get
	        {
		        if(t_user_addressbll == null)
		        {
			        t_user_addressbll = new t_user_addressBLL();
		        }
		        return t_user_addressbll;
	        }
	        set{}
        }

        
        private t_user_codeBLL t_user_codebll;
        public t_user_codeBLL t_user_codeBLL
        {
	        get
	        {
		        if(t_user_codebll == null)
		        {
			        t_user_codebll = new t_user_codeBLL();
		        }
		        return t_user_codebll;
	        }
	        set{}
        }

        
        private t_user_couponBLL t_user_couponbll;
        public t_user_couponBLL t_user_couponBLL
        {
	        get
	        {
		        if(t_user_couponbll == null)
		        {
			        t_user_couponbll = new t_user_couponBLL();
		        }
		        return t_user_couponbll;
	        }
	        set{}
        }

        
    }

}