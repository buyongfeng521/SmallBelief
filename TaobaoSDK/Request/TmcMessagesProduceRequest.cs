using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.messages.produce
    /// </summary>
    public class TmcMessagesProduceRequest : BaseTopRequest<Top.Api.Response.TmcMessagesProduceResponse>
    {
        /// <summary>
        /// tmc消息列表, 最多50条，元素结构与taobao.tmc.message.produce一致，用json表示的消息列表。例如：[{"content": "{\"tid\":1234554321,\"status\":\"X_LOGISTICS_PRINTED\",\"action_time\":\"2014-08-08 18:24:00\",\"seller_nick\": \"向阳aa\",\"operator\":\"小张\"}","topic": "taobao_jds_TradeTrace"},{"content": "{\"tid\":1234554321,\"status\":\"X_LOGISTICS_PRINTED\",\"action_time\":\"2014-08-08 18:24:00\",\"seller_nick\": \"向阳aa\",\"operator\":\"小张\"}","topic": "taobao_jds_TradeTrace"}]
        /// </summary>
        public string Messages { get; set; }

        public List<TmcPublishMessageDomain> Messages_ { set { this.Messages = TopUtils.ObjectToJson(value); } } 

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.messages.produce";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("messages", this.Messages);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("messages", this.Messages);
            RequestValidator.ValidateObjectMaxListSize("messages", this.Messages, 50);
        }

	/// <summary>
	/// TmcPublishMessageDomain Data Structure.
	/// </summary>
	[Serializable]
	public class TmcPublishMessageDomain : TopObject
	{
	        /// <summary>
	        /// 消息内容的JSON表述，必须按照topic的定义来填充
	        /// </summary>
	        [XmlElement("content")]
	        public string Content { get; set; }
	
	        /// <summary>
	        /// 消息的扩增属性，用json格式表示
	        /// </summary>
	        [XmlElement("json_ex_content")]
	        public string JsonExContent { get; set; }
	
	        /// <summary>
	        /// 直发消息需要传入目标appkey
	        /// </summary>
	        [XmlElement("target_app_key")]
	        public string TargetAppKey { get; set; }
	
	        /// <summary>
	        /// 消息类型
	        /// </summary>
	        [XmlElement("topic")]
	        public string Topic { get; set; }
	}

        #endregion
    }
}
