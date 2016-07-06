using HelperCommon;
using Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OperationManager.Controllers
{
    public class SOPartialController : Controller
    {


        /// <summary>
        /// Partial点击类型
        /// </summary>
        /// <param name="clickType"></param>
        /// <param name="clickValue"></param>
        /// <returns></returns>
        public ActionResult PartialClickType(string clickType = "", string clickValue = "")
        {
            //add
            //a 分类
            ViewBag.ListCat = SelectHelper.GetCategorySelList();
            ViewBag.ListAllType = SelectHelper.GetCategoryPlusSelList();
            //b 点击类型
            ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击);

            //other
            ViewBag.ClickGoods = "0";
            ViewBag.ClickURL = "";

            //edit
            if (!string.IsNullOrEmpty(clickType) || !string.IsNullOrEmpty(clickValue))
            {
                int iType = int.Parse(clickType);
                //a 分类
                if (iType == (int)Enums.ClickType.分类)
                {
                    ViewBag.ListCat = SelectHelper.GetCategorySelListBy(clickValue);
                }
                //b 点击类型
                ViewBag.ListClickType = SelectHelper.GetEnumSelectListItem(Enums.ClickType.不可点击, iType.ToString());
                //c 商品/URL
                if (iType == (int)Enums.ClickType.产品)
                {
                    ViewBag.ClickGoods = clickValue;
                }
                if (iType == (int)Enums.ClickType.URL)
                {
                    ViewBag.ClickURL = clickValue;
                }
            }


            return PartialView();
        }

	}
}