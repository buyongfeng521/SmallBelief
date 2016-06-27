using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonModel
{
    public class Enums
    {
        public enum CategoryType
        { 
            [Description("水果")]
            水果 = 0,
            [Description("零用品")]
            零用品 = 1,
            [Description("日用品")]
            日用品 = 2,
            [Description("微商")]
            微商 = 3,
            [Description("其他")]
            其他 = 4
        }

    }
}
