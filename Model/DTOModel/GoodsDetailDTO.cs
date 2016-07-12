using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class GoodsDetailDTO
    {
        public GoodsDTO goods { get; set; }
        public List<GoodsGalleryDTO> gallery { get; set; }
        
    }
}
