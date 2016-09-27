using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class VersionDTO
    {
        public int version { get; set; }
        public int version_lowest { get; set; }
        public string update_content { get; set; }
        public string update_url { get; set; }

    }
}
