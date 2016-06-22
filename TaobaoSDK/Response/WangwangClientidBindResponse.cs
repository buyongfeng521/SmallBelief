using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// WangwangClientidBindResponse.
    /// </summary>
    public class WangwangClientidBindResponse : TopResponse
    {
        /// <summary>
        /// 0:表示成功  其它为错误
        /// </summary>
        [XmlElement("result")]
        public long Result { get; set; }

    }
}
