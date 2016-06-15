using HelperCommon;
using Model;
using Model.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace OperationManager.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult UserList(int? index = 1, string keywords = "")
        {
            //1.0 where
            Expression<Func<t_user, bool>> where = u => u.user_phone.Contains(keywords) || u.user_name.Contains(keywords);
            //2.0 Pager
            int pageSize = 20;
            int totalCount = OperateContext.EFBLLSession.t_userBLL.GetCountBy(where);
            int pageIndex = index ?? 1;
            List<t_user> listGoods = OperateContext.EFBLLSession.t_userBLL.GetListByDesc(where, u => u.create_time);
            PagedList<t_user> mPage = listGoods.AsQueryable().ToPagedList(pageIndex, pageSize);

            mPage.TotalItemCount = totalCount;
            mPage.CurrentPageIndex = (int)(index ?? 1);
            //3.0 Result
            ViewBag.Keywords = keywords;

            return View(mPage);
        }

        [HttpGet]
        public ActionResult GetUserBy(int id = 0)
        {
            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.ID == id);
            UserDTO userDto = DTOHelper.Map<UserDTO>(user);
            userDto.create_time = ((DateTime)user.create_time).ToString("yyyy-MM-dd");
            userDto.last_login_time = ((DateTime)user.last_login_time).ToString("yyyy-MM-dd");

            return Json(userDto, JsonRequestBehavior.AllowGet);
        }


	}
}