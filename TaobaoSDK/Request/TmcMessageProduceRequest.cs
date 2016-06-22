using System;
using System.Collections.Generic;
using Top.Api.Response;
using Top.Api.Util;

namespace Top.Api.Request
{
    /// <summary>
    /// TOP API: taobao.tmc.message.produce
    /// </summary>
    public class TmcMessageProduceRequest : BaseTopRequest<TmcMessageProduceResponse>, ITopUploadRequest<TmcMessageProduceResponse>
    {
        /// <summary>
        /// 消息内容的JSON表述，必须按照topic的定义来填充
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息的扩增属性，用json格式表示
        /// </summary>
        public string ExContent { get; set; }

        /// <summary>
        /// 回传的文件内容，目前仅支持jpg,png,bmp,gif,pdf类型的文件，文件最大1M。只有消息中有byte[]类型的数据时，才需要传此字段; 否则不需要传此字段。
        /// </summary>
        public FileItem MediaContent { get; set; }

        /// <summary>
        /// 回传的文件内容，目前仅支持jpg,png,bmp,gif,pdf类型的文件，文件最大1M。只有消息中有byte[]类型的数据时，才需要传此字段; 否则不需要传此字段。具体对应到沙体中的什么值，请参考消息字段说明。
        /// </summary>
        public FileItem MediaContent2 { get; set; }

        /// <summary>
        /// 回传的文件内容，目前仅支持jpg,png,bmp,gif,pdf类型的文件，文件最大1M。只有消息中有byte[]类型的数据时，才需要传此字段; 否则不需要传此字段。具体对应到沙体中的什么值，请参考消息字段说明。
        /// </summary>
        public FileItem MediaContent3 { get; set; }

        /// <summary>
        /// 回传的文件内容，目前仅支持jpg,png,bmp,gif,pdf类型的文件，文件最大1M。只有消息中有byte[]类型的数据时，才需要传此字段; 否则不需要传此字段。具体对应到沙体中的什么值，请参考消息字段说明。
        /// </summary>
        public FileItem MediaContent4 { get; set; }

        /// <summary>
        /// 回传的文件内容，目前仅支持jpg,png,bmp,gif,pdf类型的文件，文件最大1M。只有消息中有byte[]类型的数据时，才需要传此字段; 否则不需要传此字段。具体对应到沙体中的什么值，请参考消息字段说明。
        /// </summary>
        public FileItem MediaContent5 { get; set; }

        /// <summary>
        /// 直发消息需要传入目标appkey
        /// </summary>
        public string TargetAppkey { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string Topic { get; set; }

        #region BaseTopRequest Members

        public override string GetApiName()
        {
            return "taobao.tmc.message.produce";
        }

        public override IDictionary<string, string> GetParameters()
        {
            TopDictionary parameters = new TopDictionary();
            parameters.Add("content", this.Content);
            parameters.Add("ex_content", this.ExContent);
            parameters.Add("target_appkey", this.TargetAppkey);
            parameters.Add("topic", this.Topic);
            if (this.otherParams != null)
            {
                parameters.AddAll(this.otherParams);
            }
            return parameters;
        }

        public override void Validate()
        {
            RequestValidator.ValidateRequired("content", this.Content);
            RequestValidator.ValidateMaxLength("content", this.Content, 5000);
            RequestValidator.ValidateMaxLength("ex_content", this.ExContent, 500);
            RequestValidator.ValidateMaxLength("media_content", this.MediaContent, 1048576);
            RequestValidator.ValidateMaxLength("media_content2", this.MediaContent2, 1048576);
            RequestValidator.ValidateMaxLength("media_content3", this.MediaContent3, 1048576);
            RequestValidator.ValidateMaxLength("media_content4", this.MediaContent4, 1048576);
            RequestValidator.ValidateMaxLength("media_content5", this.MediaContent5, 1048576);
            RequestValidator.ValidateMaxLength("target_appkey", this.TargetAppkey, 256);
            RequestValidator.ValidateRequired("topic", this.Topic);
            RequestValidator.ValidateMaxLength("topic", this.Topic, 256);
        }

        #endregion

        #region ITopUploadRequest Members

        public IDictionary<string, FileItem> GetFileParameters()
        {
            IDictionary<string, FileItem> parameters = new Dictionary<string, FileItem>();
            parameters.Add("media_content", this.MediaContent);
            parameters.Add("media_content2", this.MediaContent2);
            parameters.Add("media_content3", this.MediaContent3);
            parameters.Add("media_content4", this.MediaContent4);
            parameters.Add("media_content5", this.MediaContent5);
            return parameters;
        }

        #endregion
    }
}
