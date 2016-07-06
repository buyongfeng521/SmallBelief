using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class ADDTO
    {

        public int ad_id { get; set; }
        public string ad_name { get; set; }
        public byte ad_type { get; set; }
        public string ad_img { get; set; }
        public byte click_type { get; set; }
        public string click_value { get; set; }
        public byte sort { get; set; }

    }
}
