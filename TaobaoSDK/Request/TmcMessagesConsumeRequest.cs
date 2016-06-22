using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.messages.consume
    /// </summary>
    public class TmcMessagesConsumeRequest : BaseTopRequest<Top.Api.Response.TmcMessagesConsumeResponse>
    {
        /// <summary>
        /// 用户分组名称，不传表示消费默认分组，如果应用没有设置用户分组，传入分组名称将会返回错误
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 每次批量消费消息的条数
        /// </summary>
        public Nullable<long> Quantity { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.messages.consume";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("group_name", this.GroupName);
            parameters.Add("quantity", this.Quantity);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxValue("quantity", this.Quantity, 200);
            RequestValidator.ValidateMinValue("quantity", this.Quantity, 10);
        }

        #endregion
    }
}
