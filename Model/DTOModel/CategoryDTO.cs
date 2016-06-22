using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class CategoryDTO
    {
        public int cat_id { get; set; }
        public string cat_name { get; set; }
        public int cat_type { get; set; }
        public string cat_note { get; set; }
        public int sort { get; set; }
        public DateTime add_time { get; set; }
    }
}
