using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.queue.get
    /// </summary>
    public class TmcQueueGetRequest : BaseTopRequest<Top.Api.Response.TmcQueueGetResponse>
    {
        /// <summary>
        /// TMC组名
        /// </summary>
        public string GroupName { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.queue.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("group_name", this.GroupName);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("group_name", this.GroupName);
        }

        #endregion
    }
}
