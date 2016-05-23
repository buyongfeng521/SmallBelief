using DapperBLL;
using EFBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public class OperateContext
    {
        static EFBLLSession _EFBLLSession;
        public static EFBLLSession EFBLLSession
        {
            get 
            {
                return new EFBLLSession();
            }
            set
            {
                _EFBLLSession = value;
            }
        }


        


    }
}
