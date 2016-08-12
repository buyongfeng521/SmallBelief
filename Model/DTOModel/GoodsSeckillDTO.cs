using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class GoodsSeckillDTO
    {
        public string goods_begin_time { get; set; }
        public string goods_end_time { get; set; }
        public List<GoodsDTO> list_seckill_goods { get; set; }
    }
}
