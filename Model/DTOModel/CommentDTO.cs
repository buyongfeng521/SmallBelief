using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class CommentDTO
    {
        public int comment_id { get; set; }
        public int user_id { get; set; }
        public string user_name { get; set; }
        //public int order_id { get; set; }
        //public int goods_id { get; set; }
        public string comment { get; set; }
        //public bool is_del { get; set; }
        public string create_time { get; set; }
    }
}
