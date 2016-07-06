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

        /// <summary>
        /// 获得首页banner
        /// click_type(0:不可点击,1:分类,2:商品,3:URL)   
        /// click_value(点击值)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<BannerDTO>> BannerListGet()
        {
            RetInfo<List<BannerDTO>> ret = new RetInfo<List<BannerDTO>>();

            try
            {
                List<t_banner> listBanner = OperateContext.EFBLLSession.t_bannerBLL.GetListBy(b => b.banner_type == (int)Enums.BannerType.首页Banner, b => b.sort);
                ret.status = true;
                ret.Data = DTOHelper.Map<List<BannerDTO>>(listBanner);
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获得首页广告
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<ADDTO>> ADListGet()
        {
            RetInfo<List<ADDTO>> ret = new RetInfo<List<ADDTO>>();

            try
            {
                List<t_ad> listAD = OperateContext.EFBLLSession.t_adBLL.GetListBy(a => a.ad_id > 0, a => a.sort);
                ret.Data = DTOHelper.Map<List<ADDTO>>(listAD);
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
        /// 获得热销商品(前4)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> GoodsHotListGet()
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 4, g => g.is_del == false && g.is_hot == true, g => g.sort);
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
        /// 获得明星商品(前4)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<GoodsDTO>> GoodsBestListGet()
        {
            RetInfo<List<GoodsDTO>> ret = new RetInfo<List<GoodsDTO>>();

            try
            {
                //List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(g => g.is_del == false && g.is_best == true, g => g.sort);
                List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageList(1, 4, g => g.is_del == false && g.is_best == true, g => g.sort);
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
