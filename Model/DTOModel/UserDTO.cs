using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string user_name { get; set; }
        public string user_real_name { get; set; }
        public string user_psw { get; set; }
        public int user_age { get; set; }
        public string user_phone { get; set; }
        public string user_img { get; set; }
        public string token { get; set; }
        public string last_login_time { get; set; }
        public string create_time { get; set; }
    }
}
