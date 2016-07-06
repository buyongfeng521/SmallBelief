using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class ADController : Controller
    {

        public ActionResult ADList(string keywords = "")
        {
            List<t_ad> listAD = OperateContext.EFBLLSession.t_adBLL.GetListBy(a => a.ad_name.Contains(keywords), a => a.sort);

            ViewBag.Keywords = keywords;
            return View(listAD);
        }

        [HttpGet]
        public ActionResult ADAdd()
        {
            ViewBag.ListADType = SelectHelper.GetEnumSelectListItem(Enums.ADType.首页广告);

            //ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击);
            //ViewBag.ListCat = SelectHelper.GetCategorySelList();

            return View();
        }

        [HttpPost]
        public ActionResult ADAdd(string ad_name, int ad_type, HttpPostedFileBase ad_img, int sort = 0, int click_type = 0, string hideClickValue = "")
        {
            AjaxMsg ajax = new AjaxMsg();

            //1.0 check 
            if (string.IsNullOrWhiteSpace(ad_name))
            {
                ajax.Msg = "广告名称不能为空";
                return Json(ajax);
            }
            if (ad_img == null)
            {
                ajax.Msg = "广告图片不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_adBLL.GetCountBy(a => a.ad_name == ad_name.Trim()) > 0)
            {
                ajax.Msg = "已存在的广告名称";
                return Json(ajax);
            }
            //2.0 do
            string strImg = UploadHelper.UploadImage(ad_img);
            if(string.IsNullOrEmpty(strImg))
            {
                ajax.Msg = CommonBasicMsg.UploadImgFail;
                return Json(ajax);
            }
            t_ad addModel = new t_ad()
            {
                ad_name = ad_name.Trim(),
                ad_type = (byte)ad_type,
                ad_img = strImg,
                click_type = (byte)click_type,
                click_value = hideClickValue,
                sort = (byte)sort
            };
            if (OperateContext.EFBLLSession.t_adBLL.Add(addModel))
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


        [HttpGet]
        public ActionResult ADEdit(int? id)
        {
            if (id != null)
            {
                t_ad editModel = OperateContext.EFBLLSession.t_adBLL.GetModelBy(a=>a.ad_id == id);
                if (editModel != null)
                {
                    ViewBag.ListADType = SelectHelper.GetEnumSelectListItem(Enums.ADType.首页广告, editModel.ad_type.ToString());
                    //ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击,editModel.click_type.ToString());

                    //ViewBag.ListCat = SelectHelper.GetCategorySelList();

                    return View(editModel);
                }
            }

            return Redirect("ADList");
        }

        [HttpPost]
        public ActionResult ADEdit(int? ad_id, string ad_name, int ad_type, HttpPostedFileBase ad_img, int sort = 0, int click_type = 0, string hideClickValue = "")
        {
            AjaxMsg ajax = new AjaxMsg();

            //1.0 check
            if (ad_id == null)
            {
                ajax.Msg = CommonBasicMsg.VoidID;
                return Json(ajax);
            }
            t_ad editModel = OperateContext.EFBLLSession.t_adBLL.GetModelBy(a=>a.ad_id == ad_id);
            if (editModel == null)
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
                return Json(ajax);
            }
            if (string.IsNullOrWhiteSpace(ad_name))
            {
                ajax.Msg = "广告名称不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_adBLL.GetCountBy(a =>a.ad_id != ad_id && a.ad_name == ad_name.Trim()) > 0)
            {
                ajax.Msg = "已存在的广告名称";
                return Json(ajax);
            }
            //2.0 do
            string strImg = "";
            if (ad_img != null)
            {
                strImg = UploadHelper.UploadImage(ad_img);
            }
            editModel.ad_name = ad_name.Trim();
            editModel.ad_type = (byte)ad_type;
            editModel.click_type = (byte)click_type;
            editModel.click_value = hideClickValue;
            if (!string.IsNullOrEmpty(strImg))
            {
                editModel.ad_img = strImg;
            }
            if (OperateContext.EFBLLSession.t_adBLL.Modify(editModel))
            {
                ajax.Msg = CommonBasicMsg.EditSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.EditFail;
            }

            return Json(ajax);
        }

        [HttpPost]
        public ActionResult ADDel(int? id)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id != null)
            {
                if (OperateContext.EFBLLSession.t_adBLL.GetCountBy(a => a.ad_id == id) > 0)
                {
                    if (OperateContext.EFBLLSession.t_adBLL.DeleteBy(a => a.ad_id == id))
                    {
                        ajax.Msg = CommonBasicMsg.DelSuc;
                        ajax.Status = "ok";
                    }
                    else
                    {
                        ajax.Msg = CommonBasicMsg.DelFail;
                    }
                }
            }

            return Json(ajax);
        }



        #region Banner
        [HttpGet]
        public ActionResult BannerList(string keywords = "")
        {
            List<t_banner> listBanner = OperateContext.EFBLLSession.t_bannerBLL.GetListBy(b => b.banner_name.Contains(keywords), b => b.banner_type);

            ViewBag.Keywords = keywords;
            return View(listBanner);
        }

        [HttpGet]
        public ActionResult BannerAdd()
        {
            ViewBag.ListBannerType = SelectHelper.GetEnumSelectListItem(Enums.BannerType.首页Banner);

            //ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击);
            //ViewBag.ListCat = SelectHelper.GetCategorySelList();
            return View();
        }

        [HttpPost]
        public ActionResult BannerAdd(string banner_name, int banner_type, HttpPostedFileBase banner_img,int sort = 0, int click_type = 0, string hideClickValue = "")
        {
            AjaxMsg ajax = new AjaxMsg();
            //1.0 check
            if (string.IsNullOrWhiteSpace(banner_name))
            {
                ajax.Msg = "Banner名称不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_bannerBLL.GetCountBy(b => b.banner_name == banner_name.Trim()) > 0)
            {
                ajax.Msg = "Banner名称已存在";
                return Json(ajax);
            }
            if (banner_img == null)
            {
                ajax.Msg = "Banner图片不能为空";
                return Json(ajax);
            }
            //2.0 do
            string strImg = UploadHelper.UploadImage(banner_img);
            if (string.IsNullOrEmpty(strImg))
            {
                ajax.Msg = CommonBasicMsg.UploadImgFail;
                return Json(ajax);
            }

            t_banner addModel = new t_banner()
            {
                banner_name = banner_name.Trim(),
                banner_type = (byte)banner_type,
                banner_img = strImg,
                click_type = (byte)click_type,
                click_value = hideClickValue,
                sort = (byte)sort
            };


            if (OperateContext.EFBLLSession.t_bannerBLL.Add(addModel))
            {
                ajax.Msg = CommonBasicMsg.AddSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.AddFail;
            }


            //3.0 result
            return Json(ajax);
        }

        [HttpGet]
        public ActionResult BannerEdit(int? id)
        {
            if (id != null)
            {
                t_banner banner = OperateContext.EFBLLSession.t_bannerBLL.GetModelBy(b => b.banner_id == id);
                if (banner != null)
                {
                    ViewBag.ListBannerType = SelectHelper.GetEnumSelectListItem(Enums.BannerType.首页Banner, banner.banner_type.ToString());
                    ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击, banner.click_type.ToString());

                    ViewBag.ListCat = SelectHelper.GetCategorySelList();
                    ViewBag.ListCatSel = SelectHelper.GetCategorySelListBy(banner.click_value);
                    return View(banner);
                }
            }

            return Redirect("BannerList");
        }

        [HttpPost]
        public ActionResult BannerEdit(t_banner model, HttpPostedFileBase new_banner_img, int click_type = 0, string hideClickValue = "")
        {
            AjaxMsg ajax = new AjaxMsg();

            //1.0 check
            if (model == null)
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
                return Json(ajax);
            }
            t_banner editModel = OperateContext.EFBLLSession.t_bannerBLL.GetModelBy(b => b.banner_id == model.banner_id);
            if (editModel == null)
            {
                ajax.Msg = CommonBasicMsg.VoidModel;
                return Json(ajax);
            }
            if (string.IsNullOrWhiteSpace(editModel.banner_name))
            {
                ajax.Msg = "Banner名称不能为空";
                return Json(ajax);
            }
            if (OperateContext.EFBLLSession.t_bannerBLL.GetCountBy(b => b.banner_id != model.banner_id && b.banner_name == model.banner_name.Trim()) > 0)
            {
                ajax.Msg = "Banner名称已存在";
                return Json(ajax);
            }
            //2.0 do
            string strImg = "";
            if (new_banner_img != null)
            {
                strImg = UploadHelper.UploadImage(new_banner_img);
                if (string.IsNullOrEmpty(strImg))
                {
                    ajax.Msg = CommonBasicMsg.UploadImgFail;
                    return Json(ajax);
                }
            }
            editModel.banner_name = model.banner_name.Trim();
            editModel.banner_type = model.banner_type;
            if (!string.IsNullOrEmpty(strImg))
            {
                editModel.banner_img = strImg;
            }
            editModel.sort = model.sort;
            editModel.click_type = model.click_type;
            editModel.click_value = hideClickValue;

            if (OperateContext.EFBLLSession.t_bannerBLL.Modify(editModel))
            {
                ajax.Msg = CommonBasicMsg.EditSuc;
                ajax.Status = "ok";
            }
            else
            {
                ajax.Msg = CommonBasicMsg.EditFail;
            }



            return Json(ajax);
        }

        [HttpPost]
        public ActionResult BannerDel(int? id)
        {
            AjaxMsg ajax = new AjaxMsg();

            if (id != null)
            {
                if (OperateContext.EFBLLSession.t_bannerBLL.GetCountBy(b => b.banner_id == id) > 0)
                {
                    if (OperateContext.EFBLLSession.t_bannerBLL.DeleteBy(b => b.banner_id == id))
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
                    ajax.Msg = CommonBasicMsg.VoidModel;
                }
            }
            else
            {
                ajax.Msg = CommonBasicMsg.VoidID;
            }

            return Json(ajax);
        } 
        #endregion


	}
}