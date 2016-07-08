using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOModel
{
    public class CategoryGoodsDTO
    {
        public List<CatTypeDTO> CatType { get; set; }
        public List<CategoryDTO> Category { get; set; }
        public List<GoodsDTO> Goods { get; set; }
    }
}
