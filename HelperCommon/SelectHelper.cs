using Common;
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
        //分类类型
        public static SelectList GetCategoryTypeSelList()
        {
            List<t_category_type> listType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);

            SelectList result = new SelectList(listType, "cat_type_id", "type_name");

            return result;
        }
        public static SelectList GetCategoryTypeSelList(out int cat_type_id)
        {
            List<t_category_type> listType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);

            SelectList result = new SelectList(listType, "cat_type_id", "type_name");

            cat_type_id = 0;
            if (listType.Count > 0)
            {
                cat_type_id = listType[0].cat_type_id;
            }

            return result;
        }
        public static SelectList GetCategoryTypeSelList(string selectValue)
        {
            List<t_category_type> listType = OperateContext.EFBLLSession.t_category_typeBLL.GetListBy(c => c.cat_type_id >= 0, c => c.cat_type_id);

            SelectList result = new SelectList(listType, "cat_type_id", "type_name", selectValue);

            return result;
        }

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

        public static SelectList GetCategorySelList(int cat_type_id)
        {
            List<t_category> listType = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0 && c.cat_type == cat_type_id, c => c.sort);

            SelectList result = new SelectList(listType, "cat_id", "cat_name");

            return result;
        }

        

        public static SelectList GetCategorySelListBy(int cat_type_id,string selectValue)
        {
            SelectList result = null;

            List<t_category> listType = OperateContext.EFBLLSession.t_categoryBLL.GetListBy(c => c.cat_id > 0 && c.cat_type == cat_type_id, c => c.sort);

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


        /// <summary>
        /// 优惠券列表
        /// </summary>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        public static SelectList GetCouponSelList(string selectValue)
        {
            //SelectList result = null;

            //List<t_coupon> listCoupon = OperateContext.EFBLLSession.t_couponBLL.GetListBy(c => c.is_del == false, c => c.coupon_id);

            //result = new SelectList(listCoupon, "coupon_id", "coupon_name", selectValue);

            //return result;

            List<t_coupon> listCoupon = OperateContext.EFBLLSession.t_couponBLL.GetListBy(c => c.is_del == false, c => c.coupon_id);

            SelectList result = new SelectList(listCoupon, "coupon_id", "coupon_name", selectValue);

            return result;
        }

        /// <summary>
        /// 微商列表
        /// </summary>
        /// <returns></returns>
        public static SelectList GetWechatSellerList()
        {
            List<t_wechat_seller> listWechat = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetListBy(w => w.we_id > 0, w => w.we_id);
            listWechat.Insert(0, new t_wechat_seller() { we_id = 0, we_name = "无" });

            SelectList result = new SelectList(listWechat, "we_id", "we_name");

            return result;
        }
        public static SelectList GetWechatSellerList(string selectValue)
        {
            List<t_wechat_seller> listWechat = OperateContext.EFBLLSession.t_wechat_sellerBLL.GetListBy(w => w.we_id > 0, w => w.we_id);
            listWechat.Insert(0, new t_wechat_seller() { we_id = 0, we_name = "无" });

            SelectList result = new SelectList(listWechat, "we_id", "we_name",selectValue);

            return result;
        }


        #region 枚举
        /// <summary>
        /// 枚举
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumSelectListItem(Enum valueEnum)
        {
            return (from int value in Enum.GetValues(valueEnum.GetType())
                    select new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = value.ToString()
                    }).ToList();
        }
        /// <summary>
        /// 枚举选中
        /// </summary>
        /// <param name="valueEnum"></param>
        /// <param name="selectValue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetEnumSelectListItem(Enum valueEnum, string selectValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            var list = (from int value in Enum.GetValues(valueEnum.GetType())
                        select new SelectListItem
                        {
                            Text = Enum.GetName(valueEnum.GetType(), value),
                            Value = value.ToString()
                        }).ToList();
            list.ForEach(data =>
            {
                SelectListItem item = new SelectListItem();
                item.Text = data.Text;
                item.Value = data.Value;
                if (data.Value == selectValue)
                {
                    item.Selected = true;
                }
                result.Add(item);
            });

            return result;
        }

        public static List<SelectListItem> GetAllEnumSelectListItem(Enum valueEnum, string selectValue)
        {
            List<SelectListItem> result = new List<SelectListItem>();

            var list = (from int value in Enum.GetValues(valueEnum.GetType())
                        select new SelectListItem
                        {
                            Text = Enum.GetName(valueEnum.GetType(), value),
                            Value = value.ToString()
                        }).ToList();
            list.ForEach(data =>
            {
                SelectListItem item = new SelectListItem();
                item.Text = data.Text;
                item.Value = data.Value;
                if (data.Value == selectValue)
                {
                    item.Selected = true;
                }
                result.Add(item);
            });

            result.Insert(0, new SelectListItem() { Text = "全部", Value = "-1" });



            return result;
        }
        #endregion


    }
}
