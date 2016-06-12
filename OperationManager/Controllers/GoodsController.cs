using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace OperationManager.Controllers
{
    public class GoodsController : Controller
    {

        #region 商品

        [HttpGet]
        public ActionResult GoodsList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.goods_name.Contains(keywords);
            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListByDesc(where,g=>g.goods_id);
            PagedList<t_goods> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        }

        [HttpGet]
        public ActionResult GoodsAdd()
        {
            ViewBag.ListCat = SelectHelper.GetCategorySelList();
            return View();
        }

        [HttpPost]
        public ActionResult GoodsAdd(string goods_name = "", int cat_id = 0, string shop_price = "", string goods_number = "",string goods_unit = "",
            HttpPostedFileBase goods_img = null,string is_on_sale = "",string is_hot="",string is_best="",string goods_brief="")
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
            if (goods_img == null)
            {
                ajax.Msg = "商品图片不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_goodsBLL.GetCountBy(g => g.goods_name == goods_name.Trim()) > 0)
            {
                ajax.Msg = "已存在此名称的商品";
                return Json(ajax);
            }
            #endregion

            //2.0 do
            dShop_price = Math.Round(dShop_price, 2, MidpointRounding.AwayFromZero);
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
                shop_price = dShop_price,
                goods_number = iGoods_number,
                goods_lock_number = 0,
                goods_unit = goods_unit.Trim(),
                goods_img = strImg,
                is_on_sale = is_on_sale == "on"?true:false,
                is_hot = is_hot == "on"?true:false,
                is_best = is_best == "on"? true:false,
                goods_brief = goods_brief.Trim(),
                goods_desc = "",
                sort = 0,
                is_del = false,
                add_time = DateTime.Now
            };
            if (OperateContext.EFBLLSession.t_goodsBLL.Add(goodsModel))
            {
                ajax.Data = goodsModel.goods_id;
                ajax.Status = "ok";
                ajax.Msg = CommonBasicMsg.AddSuc;
            }

            return Json(ajax);
        }

        [HttpGet]
        public ActionResult GoodsEdit(int id = 0)
        {

            return View();
        }
        [HttpPost]
        public ActionResult GoodsEdit()
        {
            return View();
        }

        public ActionResult GoodsDelete(int id = 0)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id > 0)
            { 
                
            }

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
                    bool flag = true;
                    //to sql
                    for (int i = 0; i < array.Length; i++)
                    {
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
                        ajax.Data = strGallerys;
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
        

        #endregion


        #region 商品分类
        [HttpGet]
        public ActionResult CategoryList(string keywords = "")
        {
            Expression<Func<t_category, bool>> where = c => c.cat_name.Contains(keywords);
            List<t_category> listCat = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(where);

            ViewBag.Keywords = keywords;
            return View(listCat);
        }

        [HttpPost]
        public ActionResult CategoryProcess(string cat_name = "", string cat_note = "", int sort = 0,string hideCatID = "")
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

        [HttpPost]
        public ActionResult CategoryGet(int id = 0)
        {
            t_category model = new t_category();
            if (id > 0)
            {
                model = OperateContext.EFBLLSession.t_categoryBLL.GetModelBy(c=>c.cat_id == id);
            }
            return Json(model);
        }
        #endregion


	}
}