using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.CommonModel
{
    public class CommonBasicMsg
    {
        //Operate
        public const string OperateSuc = "操作成功";
        public const string OperateFail = "操作失败";
        //Add
        public const string AddSuc = "新增成功";
        public const string AddFail = "新增失败";
        //Edit
        public const string EditSuc = "修改成功";
        public const string EditFail = "修改失败";
        //Del
        public const string DelSuc = "删除成功";
        public const string DelFail = "删除失败";
        //Upload
        public const string UploadSuc = "上传成功";
        public const string UploadFail = "上传失败";
        public const string UploadImgSuc = "上传图片成功";
        public const string UploadImgFail = "上传图片失败";
        //Save
        public const string SaveSuc = "保存成功";
        public const string SaveFail = "保存失败";

        //Void
        public const string VoidID = "无效ID";
        public const string VoidModel = "无效实体";
        public const string VoidGallerys = "无效的图片集合";
        public const string VoidUser = "无效用户";
        public const string VoidAddress = "无效地址";
        public const string NoAccess = "无权限操作";
        //您所在的默认地址暂时不能提供及时送达服务。
        //阿宅撩妹去了，暂时不能进行配送。
        public const string VoidDefaultAddressGoods = "您所在的默认地址暂时不能提供及时送达服务";
        public const string VoidDefaultAddress = "阿宅撩妹去了，暂时不能进行配送";
        

        //Login
        public const string LoginSuc = "登陆成功";
        public const string LoginFail = "用户名或密码错误";
        public const string RegisterSuc = "注册成功";
        public const string RegisterFail = "注册失败";
        public const string NoLogin = "用户未登录";


        //Goods
        public const string VoidGoods = "不存在此商品";

        

        //Img


        
        //Order
        public const string CreateOrderFail = "生成订单失败";
        public const string VoidOrder = "无效订单";
        public const string PaySuc = "支付成功";
        public const string PayFail = "支付失败";
        public const string PayStatusErr = "订单不存在或支付状态异常";
        public const string OrderAmountErr = "订单金额为0";
        public const string OrderCancelErr = "订单不存在或此状态不允许取消";
        public const string OrderCancelSuc = "订单取消成功";
        public const string OrderCommentErr = "订单不存在或此状态不允许评论";
        public const string OrderCommentVoid = "评论不能为空";
        public const string OrderCommentSuc = "评论成功";


    }
}
