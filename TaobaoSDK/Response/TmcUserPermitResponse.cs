using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Top.Api.Response
{
    /// <summary>
    /// TmcUserPermitResponse.
    /// </summary>
    public class TmcUserPermitResponse : TopResponse
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        [XmlElement("is_success")]
        public bool IsSuccess { get; set; }

    }
}
