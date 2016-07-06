using HelperCommon;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Common;
using Model.DTOModel;
using Newtonsoft.Json;

namespace OperationManager.Controllers
{
    public class CommonController : Controller
    {
        /// <summary>
        /// Upload Image to QiNiu
        /// </summary>
        /// <param name="goods_img"></param>
        /// <returns></returns>
        public ActionResult UploadImages(HttpPostedFileBase goods_img = null)
        {
            string result = "";

            HttpPostedFileBase file = HttpContext.Request.Files[0];
            result = ConfigurationManager.AppSettings["Domain"] + UploadHelper.UploadImage(file);
            
            return Content(result);
        }

        [HttpGet]
        public ActionResult SearchGoodsBy(int cat_id = 0, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_goods, bool>> where = g => g.is_del == false && g.is_on_sale == true && g.goods_name.Contains(keywords);
            if (cat_id != 0)
            {
                where = where.And(g => g.cat_id == cat_id);
            }
            //if (!string.IsNullOrEmpty(keywords))
            //{
            //    where.And(g => g.goods_name.Contains(keywords));
            //}
            ////2.0 result
            List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetListBy(where);
            List<GoodsDTO> listDTO = DTOHelper.MapList<GoodsDTO>(listGoods);

            return Content(JsonConvert.SerializeObject(listDTO));
            //return Json(listDTO,JsonRequestBehavior.AllowGet);
        }


	}
}