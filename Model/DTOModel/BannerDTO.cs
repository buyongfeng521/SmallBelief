using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class BannerDTO
    {
        /// <summary>
        /// BannerID
        /// </summary>
        public int banner_id { get; set; }
        /// <summary>
        /// Banner名称
        /// </summary>
        public string banner_name { get; set; }
        public byte banner_type { get; set; }
        public string banner_img { get; set; }
        public byte click_type { get; set; }
        public string click_value { get; set; }
        public byte sort { get; set; }

    }
}
