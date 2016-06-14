using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.FormatModel
{
    public class RetInfo<T>
    {
        public RetInfo()
        {
            status = false;
            //Data = default(T);
            recordCount = 0;
            unauthorized = false;
        }
        /// <summary>操作成功或失败</summary>
        public bool status { get; set; }
        /// <summary>失败原因、操作引导信息、成功信息</summary>
        public string msg { get; set; }
        ///<summary>成功返回的数据</summary>
        public T Data { get; set; }
        /// <summary>数据记录总数</summary>
        public object recordCount { get; set; }
        /// <summary>接口操作是否未授权，若返回True，则应该重新登录</summary>
        public bool unauthorized { get; set; }
    }
}
