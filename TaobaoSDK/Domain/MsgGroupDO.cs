using System;
using System.Xml.Serialization;

namespace Top.Api.Domain
{
    /// <summary>
    /// MsgGroupDO Data Structure.
    /// </summary>
    [Serializable]
    public class MsgGroupDO : TopObject
    {
        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("appkey")]
        public string Appkey { get; set; }

        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("gmt_create")]
        public string GmtCreate { get; set; }

        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("gmt_modified")]
        public string GmtModified { get; set; }

        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("id")]
        public long Id { get; set; }

        /// <summary>
        /// 123
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
