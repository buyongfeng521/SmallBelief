using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class CartListDTO
    {
        public List<CartDTO> cart { get; set; }
        public List<GoodsDTO> goods { get; set; }
    }
}
