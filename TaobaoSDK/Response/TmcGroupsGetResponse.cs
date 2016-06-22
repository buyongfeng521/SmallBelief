using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcGroupsGetResponse.
    /// </summary>
    public class TmcGroupsGetResponse : TopResponse
    {
        /// <summary>
        /// dasdasd
        /// </summary>
        [XmlArray("groups")]
        [XmlArrayItem("tmc_group")]
        public List<Top.Api.Domain.TmcGroup> Groups { get; set; }

        /// <summary>
        /// 分组总数
        /// </summary>
        [XmlElement("total_results")]
        public long TotalResults { get; set; }

    }
}
