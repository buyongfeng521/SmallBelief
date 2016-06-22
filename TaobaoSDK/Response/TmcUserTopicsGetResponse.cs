using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcUserTopicsGetResponse.
    /// </summary>
    public class TmcUserTopicsGetResponse : TopResponse
    {
        /// <summary>
        /// 错误码
        /// </summary>
        [XmlElement("result_code")]
        public string ResultCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [XmlElement("result_message")]
        public string ResultMessage { get; set; }

        /// <summary>
        /// topic列表
        /// </summary>
        [XmlArray("topics")]
        [XmlArrayItem("string")]
        public List<string> Topics { get; set; }

    }
}
