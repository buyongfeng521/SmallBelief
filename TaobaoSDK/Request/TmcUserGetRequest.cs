using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.user.get
    /// </summary>
    public class TmcUserGetRequest : BaseTopRequest<Top.Api.Response.TmcUserGetResponse>
    {
        /// <summary>
        /// 需返回的字段列表，多个字段以半角逗号分隔。可选值：TmcUser结构体中的所有字段，一定要返回topic。
        /// </summary>
        public string Fields { get; set; }

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
            return "taobao.tmc.user.get";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("fields", this.Fields);
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
            RequestValidator.ValidateRequired("fields", this.Fields);
            RequestValidator.ValidateRequired("nick", this.Nick);
            RequestValidator.ValidateMaxLength("nick", this.Nick, 100);
        }

        #endregion
    }
}
