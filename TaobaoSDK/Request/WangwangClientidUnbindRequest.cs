using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.wangwang.clientid.unbind
    /// </summary>
    public class WangwangClientidUnbindRequest : BaseTopRequest<Top.Api.Response.WangwangClientidUnbindResponse>
    {
        /// <summary>
        /// 应用名
        /// </summary>
        public string AppName { get; set; }

        /// <summary>
        /// 客户端Id
        /// </summary>
        public string ClientId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.wangwang.clientid.unbind";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("app_name", this.AppName);
            parameters.Add("client_id", this.ClientId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("app_name", this.AppName);
            RequestValidator.ValidateRequired("client_id", this.ClientId);
        }

        #endregion
    }
}
