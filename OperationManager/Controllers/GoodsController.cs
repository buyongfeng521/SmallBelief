using HelperCommon;
using Model;
using Model.CommonModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class GoodsController : Controller
    {

        #region 商品

        [HttpGet]
        public ActionResult GoodsList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GoodsAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GoodsAdd(string goods_name)
        {
            return View();
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