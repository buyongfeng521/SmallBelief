﻿
 
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFBLL
{
    public class EFBLLSession
    {
        
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

        
    }

}