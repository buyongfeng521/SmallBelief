using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class APPHomeDTO
    {
        public List<BannerDTO> Banner { get; set; }
        public List<CatTypeDTO> Category { get; set; }
        public List<ADDTO> AD { get; set; }
        public List<GoodsDTO> GoodsHot { get; set; }
        public List<GoodsDTO> GoodsBest{get;set;}
    }
}
