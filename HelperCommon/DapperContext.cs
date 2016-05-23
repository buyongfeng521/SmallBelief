using DapperBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public class DapperContext<T> where T : class,new()
    {
        static DapperBLLBase<T> _DapperBLL;
        public static DapperBLLBase<T> DapperBLL
        {
            get
            {
                return new DapperBLLBase<T>();
            }
            set
            {
                _DapperBLL = value;
            }
        }
    }
}
