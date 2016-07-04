using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class UserAddressDTO
    {
        public string address_id { get; set; }
        public string user_id { get; set; }
        public string receipt_person { get; set; }
        public string receipt_phone { get; set; }
        public string university { get; set; }
        public string area { get; set; }
        public string building { get; set; }
        public string room_num { get; set; }
        public string address { get; set; }
        public string is_default { get; set; }

    }
}
