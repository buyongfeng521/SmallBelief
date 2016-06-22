using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.user.permit
    /// </summary>
    public class TmcUserPermitRequest : BaseTopRequest<Top.Api.Response.TmcUserPermitResponse>
    {
        /// <summary>
        /// 消息主题列表，用半角逗号分隔。当用户订阅的topic是应用订阅的子集时才需要设置，不设置表示继承应用所订阅的所有topic，一般情况建议不要设置。
        /// </summary>
        public string Topics { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.user.permit";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("topics", this.Topics);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateMaxListSize("topics", this.Topics, 100);
        }

        #endregion
    }
}
