using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcQueueGetResponse.
    /// </summary>
    public class TmcQueueGetResponse : TopResponse
    {
        /// <summary>
        /// 队列详细信息
        /// </summary>
        [XmlArray("datas")]
        [XmlArrayItem("tmc_queue_info")]
        public List<TmcQueueInfoDomain> Datas { get; set; }

	/// <summary>
	/// TmcQueueInfoDomain Data Structure.
	/// </summary>
	[Serializable]
	public class TmcQueueInfoDomain : TopObject
	{
	        /// <summary>
	        /// 消息队列Broker名称
	        /// </summary>
	        [XmlElement("broker_name")]
	        public string BrokerName { get; set; }
	
	        /// <summary>
	        /// 当前队列当天读取量
	        /// </summary>
	        [XmlElement("get_total")]
	        public long GetTotal { get; set; }
	
	        /// <summary>
	        /// TMC组名
	        /// </summary>
	        [XmlElement("name")]
	        public string Name { get; set; }
	
	        /// <summary>
	        /// 当前队列当天写入量
	        /// </summary>
	        [XmlElement("put_toal")]
	        public long PutToal { get; set; }
	}

    }
}
