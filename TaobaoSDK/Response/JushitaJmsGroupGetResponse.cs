using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// JushitaJmsGroupGetResponse.
    /// </summary>
    public class JushitaJmsGroupGetResponse : TopResponse
    {
        /// <summary>
        /// 分组信息
        /// </summary>
        [XmlArray("groups")]
        [XmlArrayItem("msg_group_d_o")]
        public List<Top.Api.Domain.MsgGroupDO> Groups { get; set; }

        /// <summary>
        /// 返回的总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
