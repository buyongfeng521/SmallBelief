using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcMessagesConsumeResponse.
    /// </summary>
    public class TmcMessagesConsumeResponse : TopResponse
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        [XmlArray("messages")]
        [XmlArrayItem("tmc_message")]
        public List<Top.Api.Domain.TmcMessage> Messages { get; set; }

    }
}
