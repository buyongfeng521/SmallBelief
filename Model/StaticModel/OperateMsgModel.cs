using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.StaticModel
{
    public class OperateMsgModel
    {
        //basic msg
        public const string AddSucMsg = "新增成功";
        public const string AddFailMsg = "新增失败";
        public const string EditSucMsg = "修改成功";
        public const string EditFailMsg = "修改失败";
        public const string DelSucMsg = "删除成功";

        //common msg
        public const string ValidID = "ID无效";
        public const string ExistsUserName = "用户名已存在";

        //cookie & session msg
        public const string SessionLoginUser = "SBeliefAdminUser";
        public const string CookieLoginUser = "CKSBeliefAdminUser";

    }
}
