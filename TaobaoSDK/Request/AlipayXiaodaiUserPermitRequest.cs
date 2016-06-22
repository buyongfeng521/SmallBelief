using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: alipay.xiaodai.user.permit
    /// </summary>
    public class AlipayXiaodaiUserPermitRequest : BaseTopRequest<Top.Api.Response.AlipayXiaodaiUserPermitResponse>
    {
        /// <summary>
        /// 用户数字ID
        /// </summary>
        public Nullable<long> UserId { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "alipay.xiaodai.user.permit";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("user_id", this.UserId);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("user_id", this.UserId);
        }

        #endregion
    }
}
