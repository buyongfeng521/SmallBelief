using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class OrderListDTO
    {
        public OrderInfoDTO order_info { get; set; }
        public List<OrderGoodsDTO> order_detail { get; set; }
    }

}
