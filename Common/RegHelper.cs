using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class RegHelper
    {
        /// <summary>
        /// 手机号码验证
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            bool result = false;

            if(!string.IsNullOrEmpty(phone))
            {
                //电信
                Regex dReg = new Regex(@"^1[3578][01379]\d{8}$");
                //联通
                Regex lReg = new Regex(@"^1[34578][01256]\d{8}$");
                //移动
                Regex yReg = new Regex(@"^(134[012345678]\d{7}|1[34578][0123456789]\d{8})$");
                if(dReg.IsMatch(phone) || lReg.IsMatch(phone) || yReg.IsMatch(phone))
                {
                    result = true;
                }

            }

            return result;
        }


    }
}
