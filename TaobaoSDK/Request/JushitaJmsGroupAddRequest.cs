using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.jushita.jms.group.add
    /// </summary>
    public class JushitaJmsGroupAddRequest : BaseTopRequest<Top.Api.Response.JushitaJmsGroupAddResponse>
    {
        /// <summary>
        /// 分组名称，同一个应用下需要保证唯一性，最长32个字符。添加分组后，消息通道会为用户的消息分配独立分组，但之前的消息还是存储于默认分组中。不能以default开头，default开头为系统默认组。
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 用户昵称列表，以半角逗号分隔，支持子账号，支持增量添加用户
        /// </summary>
        public string UserNicks { get; set; }

        /// <summary>
        /// 用户所属于的平台类型，tbUIC:淘宝用户; icbu: icbu用户
        /// </summary>
        public string UserPlatform { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.jushita.jms.group.add";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("group_name", this.GroupName);
            parameters.Add("user_nicks", this.UserNicks);
            parameters.Add("user_platform", this.UserPlatform);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("group_name", this.GroupName);
            RequestValidator.ValidateRequired("user_nicks", this.UserNicks);
            RequestValidator.ValidateMaxListSize("user_nicks", this.UserNicks, 20);
        }

        #endregion
    }
}
