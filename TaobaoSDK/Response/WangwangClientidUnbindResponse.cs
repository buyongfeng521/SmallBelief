using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// WangwangClientidUnbindResponse.
    /// </summary>
    public class WangwangClientidUnbindResponse : TopResponse
    {
        /// <summary>
        /// 返回0表示成功， 其他值为错误
        /// </summary>
        [XmlElement("result")]
        public long Result { get; set; }

    }
}
