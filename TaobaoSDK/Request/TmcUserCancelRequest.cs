using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.user.cancel
    /// </summary>
    public class TmcUserCancelRequest : BaseTopRequest<Top.Api.Response.TmcUserCancelResponse>
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 用户所属的平台类型，tbUIC:淘宝用户; icbu: icbu用户
        /// </summary>
        public string UserPlatform { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.user.cancel";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("nick", this.Nick);
            parameters.Add("user_platform", this.UserPlatform);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("nick", this.Nick);
        }

        #endregion
    }
}
