using HelperCommon;
using Model;
using Model.DTOModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    /// <summary>
    /// 商品模块
    /// </summary>
    public class GoodsController : ApiController
    {
        /// <summary>
        /// 获得所有商品分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CategoryDTO>> CategoryListGet()
        {
            RetInfo<List<CategoryDTO>> ret = new RetInfo<List<CategoryDTO>>();

            try
            {
                List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0, c => c.sort);
                if (listCat.Count > 0)
                {
                    List<CategoryDTO> listCatDTO = new List<CategoryDTO>();
                    listCatDTO = DTOHelper.MapList<CategoryDTO>(listCat);

                    ret.Data = listCatDTO;
                    ret.recordCount = listCatDTO.Count;
                    ret.msg = Message.Suc;
                }
                else
                {
                    ret.msg = Message.NullData;
                }
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 获得热销商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> GoodsHotListGet()
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(g => g.is_del == false && g.is_hot == true, g => g.goods_id);
                if (listGoods.Count > 0)
                {
                    List<GoodsDTO> listGoodsDTO = DTOHelper.MapList<GoodsDTO>(listGoods);

                    ret.Data = listGoodsDTO;
                    ret.recordCount = listGoodsDTO.Count;
                    ret.msg = Message.Suc;
                }
                else
                {
                    ret.msg = Message.NullData;
                }
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 获得明星商品
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> GoodsBestListGet()
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(g => g.is_del == false && g.is_best == true, g => g.goods_id);
                if (listGoods.Count > 0)
                {
                    List<GoodsDTO> listGoodsDTO = DTOHelper.MapList<GoodsDTO>(listGoods);

                    ret.Data = listGoodsDTO;
                    ret.recordCount = listGoodsDTO.Count;
                    ret.msg = Message.Suc;
                }
                else
                {
                    ret.msg = Message.NullData;
                }
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }






    }
}
