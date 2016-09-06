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

namespace WebApi.Controllers
{
    public class CategoryController : ApiController
    {

        ///// <summary>
        ///// 获得分类（初始）
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<CategoryGoodsDTO> CategoryGet()
        //{
        //    RetInfo<CategoryGoodsDTO> ret = new RetInfo<CategoryGoodsDTO>();

        //    try
        //    {
        //        CategoryGoodsDTO dto = new CategoryGoodsDTO();

        //        List<t_category_type> listCatType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);
        //        List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_type == 0, c => c.sort);
        //        List<int> listID = listCat.Select(c => (int)c.cat_id).ToList();
        //        List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(g => listID.Contains((int)g.cat_id), g => g.goods_id);

        //        dto.CatType = DTOHelper.Map<List<CatTypeDTO>>(listCatType);
        //        dto.Category = DTOHelper.Map<List<CategoryDTO>>(listCat);
        //        dto.Goods = DTOHelper.Map<List<GoodsDTO>>(listGoods);

        //        ret.Data = dto;
        //        ret.status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.msg = ex.ToString();
        //        Logger.WriteExceptionLog(ex);
        //    }

        //    return ret;
        //}

        ///// <summary>
        ///// 获得分类By类型（0:水果,1:零用品,2:日用品,3:微商,4:其他）
        ///// </summary>
        ///// <param name="cat_type_id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<CategoryGoodsDTO> CategoryGetBy(int cat_type_id)
        //{
        //    RetInfo<CategoryGoodsDTO> ret = new RetInfo<CategoryGoodsDTO>();

        //    try
        //    {
        //        CategoryGoodsDTO dto = new CategoryGoodsDTO();

        //        List<t_category_type> listCatType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);
        //        List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_type == cat_type_id, c => c.sort);
        //        List<int> listID = listCat.Select(c => (int)c.cat_id).ToList();
        //        List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(g => listID.Contains((int)g.cat_id), g => g.goods_id);

        //        dto.CatType = DTOHelper.Map<List<CatTypeDTO>>(listCatType);
        //        dto.Category = DTOHelper.Map<List<CategoryDTO>>(listCat);
        //        dto.Goods = DTOHelper.Map<List<GoodsDTO>>(listGoods);

        //        ret.Data = dto;
        //        ret.status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.msg = ex.ToString();
        //        Logger.WriteExceptionLog(ex);
        //    }

        //    return ret;
        //}


        /// <summary>
        /// 获得一级分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CatTypeDTO>> CategoryGet()
        {
            RetInfo<List<CatTypeDTO>> ret = new RetInfo<List<CatTypeDTO>>();

            try
            {
                List<t_category_type> listCatType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);

                ret.Data = DTOHelper.Map<List<CatTypeDTO>>(listCatType);
                ret.status = true;

            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获得二级分类（0:水果,1:零用品,2:日用品,3:微商,4:其他）
        /// </summary>
        /// <param name="cat_type_id"></param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CategoryDTO>> CategoryGetBy(int cat_type_id)
        {
            RetInfo<List<CategoryDTO>> ret = new RetInfo<List<CategoryDTO>>();

            try
            {
                List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_type == cat_type_id, c => c.sort);

                ret.Data = DTOHelper.Map<List<CategoryDTO>>(listCat);
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获得分类商品
        /// </summary>
        /// <param name="cat_id"></param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> CategoryGoodsGetBy(int cat_id,string token = "")
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.cat_id == cat_id && g.is_on_sale == true, g => g.sort);

                List<GoodsDTO> listGoodsDTO = DTOHelper.Map<List<GoodsDTO>>(listGoods);

                listGoodsDTO.ForEach(goodsDTO =>
                {
                    goodsDTO.default_address_limit = "";
                    //普通订单地址黑名单限制
                    if (goodsDTO.is_pre_sale == false)
                    {
                        if (APIHelper.IsBlacklist(token))
                        {
                            goodsDTO.default_address_limit = CommonBasicMsg.VoidDefaultAddressGoods;
                        }
                    }
                });


                ret.Data = listGoodsDTO;//DTOHelper.Map<List<GoodsDTO>>(listGoods);
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }


        /// <summary>
        /// 获取微商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<WechatSellerDTO>> WechatSellerListGet()
        {
            RetInfo<List<WechatSellerDTO>> ret = new RetInfo<List<WechatSellerDTO>>();

            try
            {
                List<t_wechat_seller> listSeller = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetListBy(s => s.we_id > 0,s=>s.sort);
                ret.Data = DTOHelper.Map<List<WechatSellerDTO>>(listSeller);
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 根据微商ID获得商品集合
        /// </summary>
        /// <param name="we_id"></param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> GoodsListGetBySellerID(int we_id)
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                List<t_goods> listSeller = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.is_del == false && g.is_on_sale == true && g.we_id == we_id, g => g.sort);
                ret.Data = DTOHelper.Map<List<GoodsDTO>>(listSeller);
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
