using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.DTOModel;
using Model.FormatModel;
using OperationManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace OperationManager.Controllers
{
    [LoginCheck]
    public class GoodsController : Controller
    {

        #region 商品

        [HttpGet]
        public ActionResult GoodsList(int? index = 1, string keywords = "", int ddlCatType = -1, int ddlCat = 0)
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.goods_name.Contains(keywords);
            if (ddlCatType != -1)
            {
                where = where.And(g => g.t_category.cat_type == ddlCatType);
            }
            if (ddlCat > 0)
            {
                where = where.And(g => g.cat_id == ddlCat);
            }


            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(where, g => g.goods_id);
            PagedList<t_goods> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;
            ViewBag.CatTypeSelList = SelectHelper.GetCategoryTypeSelListPlus(ddlCatType.ToString());


            ViewBag.ListCat = SelectHelper.GetCategoryPlusSelListBy(ddlCatType, ddlCat.ToString());

            //int cat_type_id = 0;
            //ViewBag.ListCatType = SelectHelper.GetCategoryTypeSelList(out cat_type_id);
            //ViewBag.ListCat = SelectHelper.GetCategorySelList(cat_type_id);

            //ViewBag.ListCatType = SelectHelper.GetCategoryTypeSelList(ddlCatType.ToString());
            //ddlCatType.ToString(), cat_id.ToString()
            

            return View(mPage);
        }



        [HttpGet]
        public ActionResult GoodsHotList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g =>g.is_hot == true && g.goods_name.Contains(keywords);

            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(where, g => g.goods_id);
            PagedList<t_goods> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        }


        [HttpGet]
        public ActionResult GoodsBestList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.is_best == true && g.goods_name.Contains(keywords);

            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(where, g => g.goods_id);
            PagedList<t_goods> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        }

        [HttpGet]
        public ActionResult GoodsActivityList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.is_activity == true && g.goods_name.Contains(keywords);

            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(where, g => g.goods_id);
            PagedList<t_goods> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        }

        #region 推荐商品
        [HttpGet]
        public ActionResult GoodsRecommendList(string keywords = "")
        {
            List<t_recommend_goods> listReGoods = OperateContext.EFBLLSession.t_recommend_goodsBLL.GetListBy(g => g.t_goods.goods_name.Contains(keywords), g => g.sort);

            ViewBag.ListAllType = SelectHelper.GetCategoryPlusSelList();

            return View(listReGoods);
        }

        [HttpPost]
        public ActionResult GoodsRecommendAdd(string clickGoods = "", int sort = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            //1.0 check
            if (string.IsNullOrEmpty(clickGoods))
            {
                ajax.Msg = "商品不能为空";
                return Json(ajax);
            }
            int iGoodsID = 0;
            int.TryParse(clickGoods, out iGoodsID);
            if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_id == iGoodsID) <= 0)
            {
                ajax.Msg = CommonBasicMsg.VoidGoods;
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_recommend_goodsBLL.GetCountBy(g => g.goods_id == iGoodsID) > 0)
            {
                ajax.Msg = "此推荐商品已存在";
                return Json(ajax);
            }
            //2.0 do
            t_recommend_goods addModel = new t_recommend_goods()
            {
                goods_id = iGoodsID,
                sort = sort
            };
            if (OperateContext.EFBLLSession.t_recommend_goodsBLL.Add(addModel))
            {
                ajax.Msg = CommonBasicMsg.AddSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.AddFail;
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult GoodsRecommendDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();
            ajax.Msg = CommonBasicMsg.DelFail;

            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_recommend_goodsBLL.DeleteBy(r => r.rg_id == id))
                {
                    ajax.Msg = CommonBasicMsg.DelSuc;
                    ajax.Status = "ok";
                }
            }


            return Json(ajax);
        } 
        #endregion



        #region Sales Goods
        [HttpGet]
        public ActionResult GoodsSalesList(string keywords = "")
        {
            List<t_sales_goods> listReGoods = OperateContext.EFBLLSession.t_sales_goodsBLL.GetListBy(g => g.t_goods.goods_name.Contains(keywords), g => g.sort);

            ViewBag.ListAllType = SelectHelper.GetCategoryPlusSelList();

            return View(listReGoods);
        }

        [HttpPost]
        public ActionResult GoodsSalesAdd(string clickGoods = "", int sort = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            //1.0 check
            if (string.IsNullOrEmpty(clickGoods))
            {
                ajax.Msg = "商品不能为空";
                return Json(ajax);
            }
            int iGoodsID = 0;
            int.TryParse(clickGoods, out iGoodsID);
            if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_id == iGoodsID) <= 0)
            {
                ajax.Msg = CommonBasicMsg.VoidGoods;
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_sales_goodsBLL.GetCountBy(g => g.goods_id == iGoodsID) > 0)
            {
                ajax.Msg = "此推荐商品已存在";
                return Json(ajax);
            }
            //2.0 do
            t_sales_goods addModel = new t_sales_goods()
            {
                goods_id = iGoodsID,
                sort = sort
            };
            if (OperateContext.EFBLLSession.t_sales_goodsBLL.Add(addModel))
            {
                ajax.Msg = CommonBasicMsg.AddSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.AddFail;
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult GoodsSalesDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();
            ajax.Msg = CommonBasicMsg.DelFail;

            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_sales_goodsBLL.DeleteBy(r => r.sg_id == id))
                {
                    ajax.Msg = CommonBasicMsg.DelSuc;
                    ajax.Status = "ok";
                }
            }


            return Json(ajax);
        } 
        #endregion




        [HttpGet]
        public ActionResult GoodsAdd(int goods_id = 0)
        {
            if (goods_id == 0)
            {
                int cat_type_id = 0;
                ViewBag.ListCatType = SelectHelper.GetCategoryTypeSelList(out cat_type_id);

                ViewBag.ListCat = SelectHelper.GetCategorySelList(cat_type_id);

                ViewBag.ListWechat = SelectHelper.GetWechatSellerList();

                return View(new t_goods());
            }
            else
            {
                t_goods goodsModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g=>g.goods_id == goods_id);

                //int cat_type_id = 0;
                ViewBag.ListCatType = SelectHelper.GetCategoryTypeSelList(goodsModel.t_category.cat_type.ToString());

                ViewBag.ListCat = SelectHelper.GetCategorySelListBy((int)goodsModel.t_category.cat_type,goodsModel.cat_id.ToString());

                ViewBag.ListWechat = SelectHelper.GetWechatSellerList(goodsModel.we_id.ToString());

                List<t_goods_gallery> listGallery = OperateContext.EFBLLSession.t_goods_galleryBLL.GetListBy(g=>g.goods_id == goods_id);
                ViewBag.ListGallery = listGallery;
                return View(goodsModel);
            }
            
        }

        [HttpPost]
        public ActionResult GoodsAdd(int goods_id = 0,string goods_name = "", int cat_id = 0,int we_id = 0, string shop_price = "", string goods_number = "",string goods_unit = "",
            HttpPostedFileBase goods_img = null, string is_on_sale = "", string is_hot = "", string is_best = "", string is_new = "", string is_activity = "", string is_pre_sale = "",
            int? sort = null, string goods_brief = "", string goods_brief2 = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            
            #region 1.0 check
            //1.0 check
            int iGoods_number = 0;
            decimal dShop_price = 0;
            if (string.IsNullOrEmpty(goods_name.Trim()))
            {
                ajax.Msg = "商品名称不能为空";
                return Json(ajax);
            }
            if (cat_id <= 0)
            {
                ajax.Msg = "商品分类无效";
                return Json(ajax);
            }
            if (!decimal.TryParse(shop_price, out dShop_price))
            {
                ajax.Msg = "商品价格格式错误";
                return Json(ajax);
            }
            if (!int.TryParse(goods_number, out iGoods_number))
            {
                ajax.Msg = "商品库存格式错误";
                return Json(ajax);
            }
            if (dShop_price < 0)
            {
                ajax.Msg = "商品价格不能为负数";
                return Json(ajax);
            }
            if (iGoods_number < 0)
            {
                ajax.Msg = "商品库存不能负数";
                return Json(ajax);
            }
            
            #endregion

            //2.0 do
            dShop_price = Math.Round(dShop_price, 2, MidpointRounding.AwayFromZero);

            #region I Edit
            if (goods_id > 0)
            {
                string strImg = "";
                //if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_id != goods_id && g.goods_name == goods_name.Trim()) > 0)
                //{
                //    ajax.Msg = "已存在此名称的商品";
                //    return Json(ajax);
                //}
                if (goods_img != null)
                {
                    strImg = UploadHelper.UploadImage(goods_img);
                }
                t_goods editModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == goods_id);
                if (editModel != null)
                {
                    editModel.goods_name = goods_name.Trim();
                    editModel.cat_id = cat_id;
                    editModel.we_id = we_id;
                    editModel.goods_price = dShop_price;
                    editModel.goods_number = iGoods_number;

                    editModel.goods_unit = goods_unit.Trim();
                    if (!string.IsNullOrEmpty(strImg))
                    {
                        editModel.goods_img = strImg;
                    }

                    if (sort != null)
                    {
                        editModel.sort = sort;
                    }

                    editModel.is_pre_sale = is_pre_sale == "1" ? true : false;
                    editModel.is_on_sale = is_on_sale == "true" ? true : false;
                    editModel.is_hot = is_hot == "true" ? true : false;
                    editModel.is_best = is_best == "true" ? true : false;
                    editModel.is_new = is_new == "true" ? true : false;
                    editModel.is_activity = is_activity == "true" ? true : false;
                    editModel.goods_brief = goods_brief.Trim();
                    editModel.goods_brief2 = goods_brief2.Trim();
                    if (OperateContext.EFBLLSession.t_goodsBLL.Modify(editModel))
                    {
                        ajax.Data = editModel.goods_id;
                        ajax.Status = "ok";
                        ajax.Msg = CommonBasicMsg.EditSuc;
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.EditFail;
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidGoods;
                }
            } 
            #endregion
            
            #region II Add
            else
            {
                if (goods_img == null)
                {
                    ajax.Msg = "商品图片不能为空";
                    return Json(ajax);
                }
                //if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_name == goods_name.Trim()) > 0)
                //{
                //    ajax.Msg = "已存在此名称的商品";
                //    return Json(ajax);
                //}
                string strImg = UploadHelper.UploadImage(goods_img);
                if (string.IsNullOrEmpty(strImg))
                {
                    ajax.Msg = "上传图片失败";
                    return Json(ajax);
                }
                t_goods goodsModel = new t_goods()
                {
                    goods_name = goods_name.Trim(),
                    cat_id = cat_id,
                    we_id = we_id,
                    goods_price = dShop_price,
                    goods_number = iGoods_number,
                    goods_lock_number = 0,
                    goods_unit = goods_unit.Trim(),
                    goods_img = strImg,
                    is_pre_sale = is_pre_sale == "1" ? true :false,
                    is_on_sale = is_on_sale == "on" ? true : false,
                    is_hot = is_hot == "on" ? true : false,
                    is_best = is_best == "on" ? true : false,
                    is_new = is_new == "on" ? true : false,
                    is_activity = is_activity == "on" ? true : false,
                    goods_brief = goods_brief.Trim(),
                    goods_brief2 = goods_brief2.Trim(),
                    goods_desc = "",
                    sort = sort == null?0:sort,
                    is_del = false,
                    add_time = DateTime.Now
                };
                if (OperateContext.EFBLLSession.t_goodsBLL.Add(goodsModel))
                {
                    ajax.Data = goodsModel.goods_id;
                    ajax.Status = "ok";
                    ajax.Msg = CommonBasicMsg.AddSuc;
                }
            } 
            #endregion

            return Json(ajax);
        }

        
        public ActionResult GoodsGalleryUpload(IEnumerable<HttpPostedFileBase> gallery)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (gallery.Count() > 0)
            {
                bool flag = true;
                StringBuilder sbImgs = new StringBuilder();
                foreach (HttpPostedFileBase file in gallery)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        string strImg = UploadHelper.UploadImage(file);
                        //拼接
                        if (!string.IsNullOrEmpty(strImg))
                        {
                            sbImgs.Append(strImg).Append(",");
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    ajax.Data = sbImgs.ToString();
                    ajax.Msg = CommonBasicMsg.UploadSuc;
                    ajax.Status = "ok";
                }
            }
            else
            {
                ajax.Msg = "无上传的图片";
            }

            return Json(ajax,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GoodsGalleryToData(int goods_id = 0, string gallerys = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            if (!string.IsNullOrEmpty(gallerys))
            {
                if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_id == goods_id) > 0)
                {
                    string strGallerys = gallerys.TrimEnd(',');
                    string[] array = strGallerys.Split(',');
                    StringBuilder sb = new StringBuilder();

                    bool flag = true;
                    //to sql
                    for (int i = 0; i < array.Length; i++)
                    {
                        sb.Append(ConfigurationHelper.AppSetting("Domain")).Append(array[i]).Append(",");
                        t_goods_gallery galleryModel = new t_goods_gallery()
                        {
                            goods_id = goods_id,
                            img = array[i],
                            add_time = DateTime.Now
                        };
                        if (!OperateContext.EFBLLSession.t_goods_galleryBLL.Add(galleryModel))
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        ajax.Data = sb.ToString().TrimEnd(',');
                        ajax.Msg = CommonBasicMsg.UploadSuc;
                        ajax.Status = "ok";
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidGoods;
                }
            }
            else
            {
                ajax.Msg = CommonBasicMsg.VoidGallerys;
            }
            return Json(ajax);
        }
        [HttpPost]
        public ActionResult GoodsGalleryDel(string img = "")
        {
            AjaxMsg ajax = new AjaxMsg();

            if (!string.IsNullOrEmpty(img))
            {
                if (OperateContext.EFBLLSession.t_goods_galleryBLL.GetCountBy(g => g.img == img) > 0)
                {
                    if (OperateContext.EFBLLSession.t_goods_galleryBLL.DeleteBy(g => g.img == img))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = CommonBasicMsg.DelSuc;
                    }
                }
            }

            return Json(ajax);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GoodsDetail(int goods_id = 0,string goods_detail = "")
        {
            AjaxMsg ajax = new AjaxMsg();

            if (goods_id > 0)
            {
                t_goods goodsModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g=>g.goods_id == goods_id);
                if (goodsModel != null)
                {
                    goodsModel.goods_desc = goods_detail.Trim();
                    if (OperateContext.EFBLLSession.t_goodsBLL.Modify(goodsModel))
                    {
                        ajax.Msg = CommonBasicMsg.SaveSuc;
                        ajax.Status = "ok";
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidGoods;
                }
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult GoodsDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_id == id) > 0)
                {
                    string sqlDel = @"delete from t_goods_gallery where goods_id = @goods_id
                                    delete from t_cart where goods_id = @goods_id
                                    delete from t_goods where goods_id = @goods_id";
                    if (DapperContext<t_goods>.DapperBLL.ExecuteSql(sqlDel, new { goods_id = id }))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = CommonBasicMsg.DelSuc;
                    }
                }
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult GoodsUp(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            {
                t_goods upModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g=>g.goods_id == id);
                if (upModel != null)
                {
                    upModel.is_on_sale = true;
                    if (OperateContext.EFBLLSession.t_goodsBLL.Modify(upModel))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = "上架成功";
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidGoods;
                }
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult GoodsDown(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            {
                t_goods upModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == id);
                if (upModel != null)
                {
                    upModel.is_on_sale = false;
                    if (OperateContext.EFBLLSession.t_goodsBLL.Modify(upModel))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = "下架成功";
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidGoods;
                }
            }
            
            return Json(ajax);
        }

        [HttpGet]
        public ActionResult CategoryListGet(int cat_type_id = -1)
        {
            List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_type == cat_type_id, c => c.sort);
            List<CategoryDTO> listDTO = DTOHelper.Map<List<CategoryDTO>>(listCat);
            return Json(listDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult RecommendGoods()
        {
            return View();
        }



        #endregion


        #region 商品分类
        [HttpGet]
        public ActionResult CategoryList(string keywords = "", int ddlCatType = -1)
        {
            Expression<Func<t_category, bool>> where = c => c.cat_name.Contains(keywords);
            if (ddlCatType != -1)
            {
                where = where.And(c=>c.cat_type == ddlCatType);
            }

            List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(where);

            ViewBag.TypeList = SelectHelper.GetCategoryTypeSelList();

            ViewBag.CatTypeSelList = SelectHelper.GetCategoryTypeSelListPlus(ddlCatType.ToString());

            ViewBag.Keywords = keywords;
            return View(listCat);
        }

        [HttpPost]
        public ActionResult CategoryProcess(string cat_name = "", string cat_note = "", int sort = 0, string hideCatID = "", int ddlType = 0)
        {
            AjaxMsg ajax = new AjaxMsg();
            if (string.IsNullOrEmpty(cat_name.Trim()))
            {
                ajax.Msg = "分类名称不能为空";
                return Json(ajax);
            }
            //A Add
            if (string.IsNullOrEmpty(hideCatID))
            {
                //1.0 check
                if (OperateContext.EFBLLSession.t_categoryBLL.GetCountBy(c => c.cat_name == cat_name.Trim()) > 0)
                {
                    ajax.Msg = "分类名称已存在";
                    return Json(ajax);
                }
                //2.0 do
                t_category addModel = new t_category()
                {
                    cat_name = cat_name.Trim(),
                    cat_img = "",
                    cat_note = cat_note.Trim(),
                    sort = sort,
                    cat_type = (byte)ddlType,
                    add_time = DateTime.Now
                };
                if (OperateContext.EFBLLSession.t_categoryBLL.Add(addModel))
                {
                    ajax.Status = "ok";
                    ajax.Msg = CommonBasicMsg.AddSuc;
                }
            }
            //B Edit
            else
            { 
                int iCat_id = 0;
                
                //1.0 check
                if (!int.TryParse(hideCatID, out iCat_id))
                {
                    ajax.Msg = CommonBasicMsg.VoidID;
                    return Json(ajax);
                }
                if (OperateContext.EFBLLSession.t_categoryBLL.GetCountBy(c =>c.cat_id != iCat_id && c.cat_name == cat_name.Trim()) > 0)
                {
                    ajax.Msg = "分类名称已存在";
                    return Json(ajax);
                }
                t_category editModel = OperateContext.EFBLLSession.t_categoryBLL.GetModelBy(c=>c.cat_id == iCat_id);
                if (editModel == null)
                {
                    ajax.Msg = CommonBasicMsg.VoidModel;
                    return Json(ajax);
                }
                //2.0 do
                editModel.cat_name = cat_name.Trim();
                editModel.cat_note = cat_note.Trim();
                editModel.sort = sort;
                editModel.cat_type = (byte)ddlType;
                //t_category model = new t_category() 
                //{
                //    cat_id = iCat_id,
                //    cat_name = cat_name.Trim(),
                //    cat_img = editModel.cat_img,
                //    cat_note = cat_note.Trim(),
                //    sort = sort,
                //    add_time = editModel.add_time
                //};

                if (OperateContext.EFBLLSession.t_categoryBLL.Modify(editModel))
                {
                    ajax.Status = "ok";
                    ajax.Msg = CommonBasicMsg.EditSuc;
                }

            }
           

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult CategoryDelete(int id = 0)
        { 
            AjaxMsg ajax = new AjaxMsg();
            if (id > 0)
            {
                if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.cat_id == id) > 0)
                {
                    ajax.Msg = "此分类下存在商品，不能删除";
                    return Json(ajax);
                }

                if (OperateContext.EFBLLSession.t_categoryBLL.GetCountBy(c => c.cat_id == id) > 0)
                {
                    if (OperateContext.EFBLLSession.t_categoryBLL.DeleteBy(c => c.cat_id == id))
                    {
                        ajax.Status = "ok";
                        ajax.Msg = CommonBasicMsg.DelSuc;
                    }
                }
            }
            return Json(ajax);
        }

        [HttpGet]
        public ActionResult CategoryGet(int id = 0)
        {

            t_category model = OperateContext.EFBLLSession.t_categoryBLL.GetModelBy(c => c.cat_id == id);
            if (model != null)
            {
                CategoryDTO dto = DTOHelper.Map<CategoryDTO>(model);
                return Json(dto, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }

        }
        #endregion



        #region 分类类型
        public ActionResult CatTypeList(string keywords = "")
        {
            List<t_category_type> listType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);
            //Result
            ViewBag.Keywords = keywords;
            return View(listType);
        }

        [HttpPost]
        public ActionResult CatTypeSave(int cat_type_id = -1, string type_name = "", HttpPostedFileBase type_img = null)
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            //if (cat_type_id < 0)
            //{
            //    ajax.Msg = CommonBasicMsg.VoidModel;
            //    return Json(ajax);
            //}
            if (string.IsNullOrWhiteSpace(type_name))
            {
                ajax.Msg = "名称不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_category_typeBLL.GetCountBy(c => c.cat_type_id != cat_type_id && c.type_name == type_name.Trim()) > 0)
            {
                ajax.Msg = "已存在的名称";
                return Json(ajax);
            }
            //2.0 do
            //a edit
            if (cat_type_id >= 0)
            {
                t_category_type editModel = OperateContext.EFBLLSession.t_category_typeBLL.GetModelBy(c => c.cat_type_id == cat_type_id);
                if (editModel != null)
                {
                    string strImg = "";
                    if (type_img != null)
                    {
                        strImg = UploadHelper.UploadImage(type_img);
                    }
                    //img
                    if (!string.IsNullOrWhiteSpace(strImg))
                    {
                        editModel.type_img = strImg;
                    }
                    editModel.type_name = type_name.Trim();
                    if (OperateContext.EFBLLSession.t_category_typeBLL.Modify(editModel))
                    {
                        ajax.Msg = CommonBasicMsg.EditSuc;
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.EditFail;
                    }
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidModel;
                }
            }
            //add
            else
            {
                if (type_img == null)
                {
                    ajax.Msg = "图片不能为空";
                    return Json(ajax);
                }
                string strImg = UploadHelper.UploadImage(type_img);
                if (string.IsNullOrEmpty(strImg))
                {
                    ajax.Msg = CommonBasicMsg.UploadImgFail;
                    return Json(ajax);
                }
                //DO
                int id = 0;
                List<t_category_type> listModel = OperateContext.EFBLLSession.t_category_typeBLL.GetListByDesc(c => c.cat_type_id >= 0, c => c.cat_type_id);
                if (listModel.Count > 0)
                {
                    id = listModel[0].cat_type_id + 1;
                }
                t_category_type modelAdd = new t_category_type() 
                {
                    cat_type_id = id,
                    type_name = type_name.Trim(),
                    type_img = strImg
                };
                if (OperateContext.EFBLLSession.t_category_typeBLL.Add(modelAdd))
                {
                    ajax.Msg = CommonBasicMsg.AddSuc;
                    ajax.Status = "ok";
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.AddFail;
                }
            }
            
            return Json(ajax);
        }

        [HttpPost]
        public ActionResult CatTypeDelete(int cat_type_id = -1)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (OperateContext.EFBLLSession.t_category_typeBLL.GetCountBy(c => c.cat_type_id == cat_type_id) <= 0)
            {
                ajax.Msg = "此类型不存在";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_categoryBLL.GetCountBy(c => c.cat_type == cat_type_id) > 0)
            {
                ajax.Msg = "此类型下面有所属商品分类，无法删除";
                return Json(ajax);
            }

            if (OperateContext.EFBLLSession.t_category_typeBLL.DeleteBy(c => c.cat_type_id == cat_type_id))
            {
                ajax.Msg = CommonBasicMsg.DelSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.DelFail;
            }
            

            return Json(ajax);
        }

        [HttpGet]
        public ActionResult CatTypeModelGet(int cat_type_id = -1)
        {
            t_category_type catModel = OperateContext.EFBLLSession.t_category_typeBLL.GetModelBy(c => c.cat_type_id == cat_type_id);
            if (catModel != null)
            {
                CatTypeDTO dto = DTOHelper.Map<CatTypeDTO>(catModel);
                return Json(dto, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        } 
        #endregion


        #region 微商管理
        public ActionResult WechatSellerList(string keywords = "")
        {
            List<t_wechat_seller> listWechat = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetListBy(w => w.we_name.Contains(keywords));

            //result
            ViewBag.Keywords = keywords;
            return View(listWechat);
        }

        [HttpPost]
        public ActionResult WechatAdd(int we_id = 0, string we_name = "", int we_star = 0, int sort = 0,HttpPostedFileBase we_img = null, string we_desc = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrWhiteSpace(we_name))
            {
                ajax.Msg = "微商名称不能为空";
                return Json(ajax);
            }
            //2.0 do
            //Edit
            if (we_id > 0)
            {
                t_wechat_seller editModel = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetModelBy(w => w.we_id == we_id);
                if (editModel != null)
                {
                    string strImg = "";
                    if (we_img != null)
                    {
                        strImg = UploadHelper.UploadImage(we_img);
                    }
                    editModel.we_name = we_name;
                    editModel.we_star = we_star;
                    editModel.sort = sort;
                    if (!string.IsNullOrEmpty(strImg))
                    {
                        editModel.we_img = strImg;
                    }
                    editModel.we_desc = we_desc;
                    if (OperateContext.EFBLLSession.t_wechat_sellerBLL.Modify(editModel))
                    {
                        ajax.Msg = CommonBasicMsg.EditSuc;
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.EditFail;
                    }

                }
                else
                {
                    ajax.Msg = CommonBasicMsg.VoidModel;
                }
            }
            //Add
            else
            {
                if (we_img == null)
                {
                    ajax.Msg = "微商图片不能为空";
                    return Json(ajax);
                }
                string strImg = UploadHelper.UploadImage(we_img);
                if (string.IsNullOrEmpty(strImg))
                {
                    ajax.Msg = CommonBasicMsg.UploadImgFail;
                    return Json(ajax);
                }
                t_wechat_seller modelAdd = new t_wechat_seller()
                {
                    we_name = we_name.Trim(),
                    we_star = we_star,
                    sort = sort,
                    we_img = strImg,
                    we_desc = we_desc
                };
                if (OperateContext.EFBLLSession.t_wechat_sellerBLL.Add(modelAdd))
                {
                    ajax.Status = "ok";
                    ajax.Msg = CommonBasicMsg.AddSuc;
                }
                else
                {
                    ajax.Msg = CommonBasicMsg.AddFail;
                }
            }

            return Json(ajax);

        }

        [HttpPost]
        public ActionResult WechartDelete(int we_id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (OperateContext.EFBLLSession.t_wechat_sellerBLL.GetCountBy(w => w.we_id == we_id) > 0)
            {
                if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.we_id == we_id) <= 0)
                {
                    if (OperateContext.EFBLLSession.t_wechat_sellerBLL.DeleteBy(w => w.we_id == we_id))
                    {
                        ajax.Msg = CommonBasicMsg.DelSuc;
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.DelFail;
                    }
                }
                else
                {
                    ajax.Msg = "有部分商品依赖此微商，请处理后再删除";
                }
            }
            else
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
            }

            return Json(ajax);
        }


        [HttpGet]
        public ActionResult WechartModelGet(int we_id = 0)
        {
            t_wechat_seller wechatModel = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetModelBy(w => w.we_id == we_id);
            if (wechatModel != null)
            {
                WechatSellerDTO dto = DTOHelper.Map<WechatSellerDTO>(wechatModel);
                return Json(dto, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;
            }
        } 
        #endregion



	}
}