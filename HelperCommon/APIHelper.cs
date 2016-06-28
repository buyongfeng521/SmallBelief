using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public class APIHelper
    {
        public static bool IsLogin(string token)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(token))
            {
                if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.token == token) > 0)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
