using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// JushitaJmsGroupAddResponse.
    /// </summary>
    public class JushitaJmsGroupAddResponse : TopResponse
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlElement("created")]
        public string Created { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [XmlElement("group_name")]
        public string GroupName { get; set; }

    }
}
