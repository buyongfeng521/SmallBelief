using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class EnumHelper
    {
        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public static string GetEnumDesc(this Enum en)
        {
            Type type = en.GetType();
            MemberInfo[] memInfo = type.GetMember(en.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return en.ToString();
        }

        /// <summary>
        /// 枚举array
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(this Type type)
        {
            if (type.IsEnum)
            {
                ArrayList _array = new ArrayList();
                Array _enumValues = Enum.GetValues(type);
                foreach (Enum value in _enumValues)
                {
                    _array.Add(new KeyValuePair<Enum, string>(value, GetEnumDesc(value)));
                }
                return _array;
            }
            return null;
        }
    }
}
