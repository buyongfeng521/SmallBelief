using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HelperCommon
{
    public class SelectHelper
    {
        #region 分类
        //分类
        public static SelectList GetCategorySelList()
        {
            List<t_category> listType = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0, c => c.sort);

            SelectList result = new SelectList(listType, "cat_id", "cat_name");

            return result;
        }

        public static SelectList GetCategorySelListBy(string selectValue)
        {
            SelectList result = null;

            List<t_category> listType = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0, c => c.sort);

            result = new SelectList(listType, "cat_id", "cat_name", selectValue);

            return result;
        }

        //分类（包含全部）
        public static SelectList GetCategoryPlusSelList()
        {
            SelectList result = null;

            List<t_category> listType = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0, c => c.sort);
         
            listType.Insert(0, new t_category() { cat_id = 0, cat_name = "全部分类" });
            result = new SelectList(listType, "cat_id", "cat_name");

            return result;
        }
        #endregion
    }
}
