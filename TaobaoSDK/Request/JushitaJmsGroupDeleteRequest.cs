using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.jushita.jms.group.delete
    /// </summary>
    public class JushitaJmsGroupDeleteRequest : BaseTopRequest<Top.Api.Response.JushitaJmsGroupDeleteResponse>
    {
        /// <summary>
        /// 分组名称，分组删除后，用户的消息将会存储于默认分组中。警告：由于分组已经删除，用户之前未消费的消息将无法再获取。不能以default开头，default开头为系统默认组。
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 用户列表，不传表示删除整个分组，如果用户全部删除后，也会自动删除整个分组
        /// </summary>
        public string Nicks { get; set; }

        /// <summary>
        /// 用户所属于的平台类型，tbUIC:淘宝用户; icbu: icbu用户
        /// </summary>
        public string UserPlatform { get; set; }

        #region ITopRequest Members

        public override string GetApiName()
        {
            return "taobao.jushita.jms.group.delete";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("group_name", this.GroupName);
            parameters.Add("nicks", this.Nicks);
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
            RequestValidator.ValidateMaxListSize("nicks", this.Nicks, 20);
        }

        #endregion
    }
}
