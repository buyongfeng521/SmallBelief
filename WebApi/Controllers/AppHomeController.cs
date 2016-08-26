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
    public class AppHomeController : ApiController
    {

        #region Cancel
        ///// <summary>
        ///// 获得首页banner
        ///// click_type(0:不可点击,1:分类,2:商品,3:URL)   
        ///// click_value(点击值)
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<List<BannerDTO>> BannerListGet()
        //{
        //    RetInfo<List<BannerDTO>> ret = new RetInfo<List<BannerDTO>>();

        //    try
        //    {
        //        List<t_banner> listBanner = OperateContext.EFBLLSession.t_bannerBLL.GetListBy(b => b.banner_type == (int)Enums.BannerType.首页Banner, b => b.sort);
        //        ret.status = true;
        //        ret.Data = DTOHelper.Map<List<BannerDTO>>(listBanner);
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.msg = ex.ToString();
        //        Logger.WriteExceptionLog(ex);
        //    }

        //    return ret;
        //}

        ///// <summary>
        ///// 获得首页广告
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<List<ADDTO>> ADListGet()
        //{
        //    RetInfo<List<ADDTO>> ret = new RetInfo<List<ADDTO>>();

        //    try
        //    {
        //        List<t_ad> listAD = OperateContext.EFBLLSession.t_adBLL.GetListBy(a => a.ad_id > 0, a => a.sort);
        //        ret.Data = DTOHelper.Map<List<ADDTO>>(listAD);
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
        ///// 获得热销商品(前4)
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<List<GoodsDTO>> GoodsHotListGet()
        //{
        //    RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

        //    try
        //    {
        //        List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 4, g => g.is_del == false && g.is_hot == true, g => g.sort);
        //        if (listGoods.Count > 0)
        //        {
        //            List<GoodsDTO> listGoodsDTO = DTOHelper.MapList<GoodsDTO>(listGoods);

        //            ret.Data = listGoodsDTO;
        //            ret.recordCount = listGoodsDTO.Count;
        //            ret.msg = Message.Suc;
        //        }
        //        else
        //        {
        //            ret.msg = Message.NullData;
        //        }
        //        ret.status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.msg = ex.ToString();
        //    }

        //    return ret;
        //}

        ///// <summary>
        ///// 获得明星商品(前4)
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public RetInfo<List<GoodsDTO>> GoodsBestListGet()
        //{
        //    RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

        //    try
        //    {
        //        //List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.is_del == false && g.is_best == true, g => g.sort);
        //        List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 4, g => g.is_del == false && g.is_best == true, g => g.sort);
        //        if (listGoods.Count > 0)
        //        {
        //            List<GoodsDTO> listGoodsDTO = DTOHelper.MapList<GoodsDTO>(listGoods);

        //            ret.Data = listGoodsDTO;
        //            ret.recordCount = listGoodsDTO.Count;
        //            ret.msg = Message.Suc;
        //        }
        //        else
        //        {
        //            ret.msg = Message.NullData;
        //        }
        //        ret.status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ret.msg = ex.ToString();
        //    }

        //    return ret;
        //} 
        #endregion


        /// <summary>
        /// 首页
        /// click_type(0:不可点击,1:分类,2:商品,3:URL)   
        /// click_value(点击值)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<APPHomeDTO> APPHomeGet()
        {
            RetInfo<APPHomeDTO> ret = new RetInfo<APPHomeDTO>();

            try
            {
                APPHomeDTO dto = new APPHomeDTO();
                //1.0 basic
                List<t_banner> listBanner = OperateContext.EFBLLSession.t_bannerBLL.GetListBy(b => b.banner_type == (int)Enums.BannerType.首页Banner, b => b.sort);
                List<t_category_type> listCatType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);
                List<t_ad> listAD = OperateContext.EFBLLSession.t_adBLL.GetListBy(a => a.ad_id > 0, a => a.sort);
                List<t_goods> listGoodsHot = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 6, g => g.is_del == false && g.is_hot == true, g => g.sort);
                List<t_goods> listGoodsBest = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 6, g => g.is_del == false && g.is_best == true, g => g.sort);
                //秒杀
                List<t_goods> listSkill = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 6, g => g.is_del == false && g.is_activity == true, g => g.sort);
                t_setting begin_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_begin_time");
                t_setting end_time_model = OperateContext.EFBLLSession.t_settingBLL.GetModelBy(s => s.set_key == "goods_end_time");
                GoodsSeckillDTO seckillDTO = new GoodsSeckillDTO();
                seckillDTO.goods_begin_time = begin_time_model.set_value;
                seckillDTO.goods_end_time = end_time_model.set_value;
                seckillDTO.list_seckill_goods = DTOHelper.Map<List<GoodsDTO>>(listSkill);


                //2.0 to dto
                dto.Banner = DTOHelper.Map<List<BannerDTO>>(listBanner);
                dto.Category = DTOHelper.Map<List<CatTypeDTO>>(listCatType);
                dto.AD = DTOHelper.Map<List<ADDTO>>(listAD);
                dto.GoodsSeckill = seckillDTO;
                dto.GoodsHot = DTOHelper.MapList<GoodsDTO>(listGoodsHot);
                dto.GoodsBest = DTOHelper.MapList<GoodsDTO>(listGoodsBest);
                //3.0 result
                ret.Data = dto;
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
