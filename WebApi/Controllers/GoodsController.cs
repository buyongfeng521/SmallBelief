using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
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
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.is_del == false && g.is_hot == true, g => g.sort);
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
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.is_del == false && g.is_best == true, g => g.sort);
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
        /// 获得商品明细根据ID
        /// </summary>
        /// <param name="goods_id">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<GoodsDetailDTO> GoodsDetailGet(int goods_id)
        {
            RetInfo<GoodsDetailDTO> ret = new RetInfo<GoodsDetailDTO>();

            try
            {
                GoodsDetailDTO dto = new GoodsDetailDTO();
                t_goods model = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == goods_id);
                if (model != null)
                {
                    dto.goods = DTOHelper.Map<GoodsDTO>(model);
                    List<t_goods_gallery> listGallery = OperateContext.EFBLLSession.t_goods_galleryBLL.GetListBy(g => g.goods_id == model.goods_id);
                    List<GoodsGalleryDTO> listGalleryDTO = DTOHelper.Map<List<GoodsGalleryDTO>>(listGallery);
                    dto.gallery = listGalleryDTO.Select(g => g.img).ToArray();

                    ret.Data = dto;
                    ret.status = true;
                }
                else
                {
                    ret.msg = CommonBasicMsg.VoidGoods;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }


        /// <summary>
        /// 获得商品评价
        /// </summary>
        /// <param name="goods_id">商品ID</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CommentDTO>> GoodsCommentGet(int goods_id)
        {
            RetInfo<List<CommentDTO>> ret = new RetInfo<List<CommentDTO>>();
            try
            {
                List<t_comment> listComment = OperateContext.EFBLLSession.t_commentBLL.GetListByDesc(c => c.goods_id == goods_id && c.is_del == false, c => c.create_time);
                ret.Data = DTOHelper.Map<List<CommentDTO>>(listComment);
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }


    }

}
