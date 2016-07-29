using Common;
using HelperCommon;
using Model;
using Model.CommonModel;
using Model.DTOModel;
using Model.FormatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{

    public class MineController : ApiController
    {
        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="obj">{"token":"用户Token","nickname":"昵称"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserNickNameModify(dynamic obj)
        { 
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                string nickname = obj.nickname;
                if (APIHelper.IsLogin(token))
                {
                    if (!string.IsNullOrWhiteSpace(nickname))
                    {
                        t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                        if (user != null)
                        {
                            if (OperateContext.EFBLLSession.t_userBLL.GetCountBy(u => u.ID != user.ID && u.user_name == nickname.Trim()) <= 0)
                            {
                                user.user_name = nickname.Trim();
                                if (OperateContext.EFBLLSession.t_userBLL.Modify(user))
                                {
                                    ret.msg = CommonBasicMsg.EditSuc;
                                    ret.status = true;
                                    user.user_img = ConfigurationHelper.AppSetting("Domain") + user.user_img;
                                    ret.Data = user;
                                }
                                else
                                {
                                    ret.msg = CommonBasicMsg.EditFail;
                                }
                            }
                            else
                            {
                                ret.msg = "已存在此昵称";
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidUser;
                        }
                    }
                    else
                    {
                        ret.msg = "昵称不能为空";
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="obj">{"token":"用户token","old_psw":"旧密码","new_psw":"新密码"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserPswModify(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                //string token, string old_psw, string new_psw
                string token = obj.token;
                string old_psw = obj.old_psw;
                string new_psw = obj.new_psw;
                if (APIHelper.IsLogin(token))
                {


                    if (old_psw != null && !string.IsNullOrEmpty(old_psw.Trim()) && new_psw != null && !string.IsNullOrEmpty(new_psw.Trim()))
                    {
                        t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                        if (user != null)
                        {
                            if (user.user_psw == SecurityHelper.GetMD5(old_psw.Trim()))
                            {
                                user.user_psw = SecurityHelper.GetMD5(new_psw.Trim());
                                if (OperateContext.EFBLLSession.t_userBLL.Modify(user))
                                {
                                    ret.msg = CommonBasicMsg.EditSuc;
                                    ret.status = true;
                                    user.user_img = ConfigurationHelper.AppSetting("Domain") + user.user_img;
                                    ret.Data = user;
                                }
                                else
                                {
                                    ret.msg = CommonBasicMsg.EditFail;
                                }
                            }
                            else
                            {
                                ret.msg = "旧密码错误";
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidUser;
                        }
                    }
                    else
                    {
                        ret.msg = "请输入有效的密码";
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="obj">{"token":"用户Token","user_img":"用户头像（七牛）"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserImgModify(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                string user_img = obj.user_img;
                if (APIHelper.IsLogin(token))
                {
                    if (!string.IsNullOrWhiteSpace(user_img))
                    {
                        t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                        if (user != null)
                        {
                            user.user_img = user_img;
                            if (OperateContext.EFBLLSession.t_userBLL.Modify(user))
                            {
                                ret.msg = CommonBasicMsg.EditSuc;
                                ret.status = true;
                                user.user_img = ConfigurationHelper.AppSetting("Domain") + user.user_img;
                                ret.Data = user;
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.EditFail;
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidUser;
                        }
                    }
                    else
                    {
                        ret.msg = "请上传有效的头像";
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获取用户默认收货地址
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<UserAddressDTO> UserAddressDefaultGet(string token)
        {
            RetInfo<UserAddressDTO> ret = new RetInfo<UserAddressDTO>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        t_user_address addressModel = OperateContext.EFBLLSession.t_user_addressBLL.GetModelBy(a => a.user_id == user.ID && a.is_default == true);
                        if (addressModel != null)
                        {
                            ret.Data = DTOHelper.Map<UserAddressDTO>(addressModel);
                        }
                        else
                        {
                            ret.msg = Message.NullData;
                        }

                        ret.status = true;
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        /// <param name="token">用户token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<UserAddressDTO>> UserAddressGet(string token)
        {
            RetInfo<List<UserAddressDTO>> ret = new RetInfo<List<UserAddressDTO>>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        List<t_user_address> listAddress = OperateContext.EFBLLSession.t_user_addressBLL.GetListByDesc(a => a.user_id == user.ID, a => a.is_default);
                        if (listAddress.Count > 0)
                        {
                            ret.Data = DTOHelper.Map<List<UserAddressDTO>>(listAddress);
                        }
                        else
                        {
                            ret.msg = Message.NullData;
                        }

                        ret.status = true;
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 用户地址新增
        /// </summary>
        /// <param name="obj">{"token":"用户token","receipt_person":"收货人姓名","receipt_phone":"收货人手机号码","area":"区号","building":"楼号","room_num":"寝室号","is_default":true/false}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserAddressAdd(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                string receipt_person = obj.receipt_person;
                string receipt_phone = obj.receipt_phone;
                string area = obj.area;
                string building = obj.building;
                string room_num = obj.room_num;
                bool is_default = obj.is_default;
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        if (!string.IsNullOrWhiteSpace(receipt_person) && !string.IsNullOrWhiteSpace(receipt_phone))
                        {
                            if (RegHelper.IsPhone(receipt_phone))
                            {
                                if (!string.IsNullOrWhiteSpace(area) && !string.IsNullOrWhiteSpace(building) && !string.IsNullOrWhiteSpace(room_num))
                                {
                                    t_user_address user_address = new t_user_address()
                                    {
                                        user_id = user.ID,
                                        receipt_person = receipt_person,
                                        receipt_phone = receipt_phone,
                                        university = "浙江理工大学",
                                        area = area,
                                        building = building,
                                        room_num = room_num,
                                        address = "浙江理工大学" + area + building + room_num,
                                        is_default = is_default == true ? true : false
                                    };
                                    if (OperateContext.EFBLLSession.t_user_addressBLL.Add(user_address))
                                    {
                                        if (user_address.is_default == true)
                                        {
                                            string upSql = "update t_user_address set is_default = 0 where address_id <> @address_id and user_id = @user_id";
                                            DapperContext<t_user_address>.DapperBLL.ExecuteSql(upSql, new { address_id = user_address.address_id, user_id = user.ID });
                                        }

                                        ret.msg = CommonBasicMsg.AddSuc;
                                        ret.status = true;
                                    }
                                    else
                                    {
                                        ret.msg = CommonBasicMsg.AddFail;
                                    }
                                }
                                else
                                {
                                    ret.msg = Message.VoidAddress;
                                }
                            }
                            else
                            {
                                ret.msg = Message.VoidPhone;
                            }
                        }
                        else
                        {
                            ret.msg = "请输入收货人姓名和手机号码";
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }


            return ret;
        }

        /// <summary>
        /// 用户地址修改
        /// </summary>
        /// <param name="obj">{"address_id":"用户地址ID","receipt_person":"收货人姓名","receipt_phone":"收货人手机号码","area":"区号","building":"楼号","room_num":"寝室号","is_default":true/false}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserAddressEdit(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string address_id = obj.address_id;
                string receipt_person = obj.receipt_person;
                string receipt_phone = obj.receipt_phone;
                string area = obj.area;
                string building = obj.building;
                string room_num = obj.room_num;
                bool is_default = obj.is_default;

                if (!string.IsNullOrWhiteSpace(receipt_person) && !string.IsNullOrWhiteSpace(receipt_phone))
                {
                    if (RegHelper.IsPhone(receipt_phone))
                    {
                        if (!string.IsNullOrWhiteSpace(area) && !string.IsNullOrWhiteSpace(building) && !string.IsNullOrWhiteSpace(room_num))
                        {
                            int iAddress_id = 0;
                            if (int.TryParse(address_id, out iAddress_id))
                            {
                                t_user_address editModel = OperateContext.EFBLLSession.t_user_addressBLL.GetModelBy(a => a.address_id == iAddress_id);
                                if (editModel != null)
                                {
                                    editModel.receipt_person = receipt_person;
                                    editModel.receipt_phone = receipt_phone;
                                    editModel.university = "浙江理工大学";
                                    editModel.area = area;
                                    editModel.building = building;
                                    editModel.room_num = room_num;
                                    editModel.address = "浙江理工大学" + area + building + room_num;
                                    is_default = is_default == true ? true : false;
                                    if (OperateContext.EFBLLSession.t_user_addressBLL.Modify(editModel))
                                    {
                                        if (editModel.is_default == true)
                                        {
                                            string upSql = "update t_user_address set is_default = 1 where address_id <> @address_id and user_id = @user_id";
                                            DapperContext<t_user_address>.DapperBLL.ExecuteSql(upSql, new { address_id = editModel.address_id, user_id = editModel.user_id });
                                        }

                                        ret.msg = CommonBasicMsg.EditSuc;
                                        ret.status = true;
                                    }
                                    else
                                    {
                                        ret.msg = CommonBasicMsg.EditFail;
                                    }
                                }
                                else
                                {
                                    ret.msg = CommonBasicMsg.VoidModel;
                                }
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.VoidID;
                            }

                        }
                        else
                        {
                            ret.msg = Message.VoidAddress;
                        }
                    }
                    else
                    {
                        ret.msg = Message.VoidPhone;
                    }
                }
                else
                {
                    ret.msg = "请输入收货人姓名和手机号码";
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 用户地址删除
        /// </summary>
        /// <param name="obj">{"token":"用户Token","address_id":"用户地址ID"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> UserAddressDelete(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                string address_id = obj.address_id;
                int iAddress_id = 0;
                if (APIHelper.IsLogin(token))
                {
                    if (int.TryParse(address_id, out iAddress_id))
                    {
                        if (OperateContext.EFBLLSession.t_user_addressBLL.GetCountBy(a => a.address_id == iAddress_id) > 0)
                        {
                            if (OperateContext.EFBLLSession.t_user_addressBLL.DeleteBy(a => a.address_id == iAddress_id))
                            {
                                ret.msg = CommonBasicMsg.DelSuc;
                                ret.status = true;
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.DelFail;
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidModel;
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidID;
                    }
                }
                else
                {
                    ret.msg = Message.NoAccess;
                }
            }
            catch(Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 用户优惠券
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<UserCouponDTO>> UserCouponsGet(string token)
        {
            RetInfo<List<UserCouponDTO>> ret = new RetInfo<List<UserCouponDTO>>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        List<t_user_coupon> listCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetListByDesc(a => a.user_id == user.ID, a => a.end_time);
                        if (listCoupon.Count > 0)
                        {
                            ret.Data = DTOHelper.Map<List<UserCouponDTO>>(listCoupon);
                        }
                        else
                        {
                            ret.msg = Message.NullData;
                        }

                        ret.status = true;
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidUser;
                    }
                }
                else
                {
                    ret.msg = Message.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }


        /// <summary>
        /// 获取学校内区号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<string>> AddressAreaGet()
        {
            RetInfo<List<string>> ret = new RetInfo<List<string>>();

            try
            {
                string sql = "select distinct area from t_room  order by Area";
                List<string> listArea = DapperContext<t_room>.DapperBLL.QueryListSql(sql, null).Select(r => r.area).ToList();
                ret.Data = listArea;
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }
            
            return ret;
        }

        /// <summary>
        /// 获取楼号根据区号
        /// </summary>
        /// <param name="area">区号</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<string>> AddressBuildingGet(string area)
        {
            RetInfo<List<string>> ret = new RetInfo<List<string>>();

            try
            {
                string sql = "select distinct building from t_room where area = @area order by building";
                List<string> listBuilding = DapperContext<t_room>.DapperBLL.QueryListSql(sql, new { area = area }).Select(r => r.building).ToList();
                ret.Data = listBuilding;
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 获取寝室号根据区号和楼号
        /// </summary>
        /// <param name="area"></param>
        /// <param name="building"></param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<string>> AddressRoomGet(string area, string building)
        {
            RetInfo<List<string>> ret = new RetInfo<List<string>>();

            try
            {
                string sql = "select room_num from t_room where area = @area and building = @building order by room_num";
                List<string> listRoom = DapperContext<t_room>.DapperBLL.QueryListSql(sql, new { area = area, building = building }).Select(r => r.room_num).ToList();
                ret.Data = listRoom;
                ret.status = true;
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 全部订单
        /// </summary>
        /// <param name="token">用户token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<OrderListDTO>> OrderListAllGet(string token)
        {
            RetInfo<List<OrderListDTO>> ret = new RetInfo<List<OrderListDTO>>();

            try
            {
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.token == token);
                if (user != null)
                {


                    List<OrderListDTO> dto = new List<OrderListDTO>();
                    List<t_order_info> listOrder = OperateContext.EFBLLSession.t_order_infoBLL.GetListByDesc(o => o.user_id == user.ID, o => o.add_time);
                    listOrder.ForEach(data =>
                    {
                        OrderListDTO itemDTO = new OrderListDTO();
                        List<t_order_goods> listGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == data.order_id);
                        itemDTO.order_info = DTOHelper.Map<OrderInfoDTO>(data);
                        itemDTO.order_detail = DTOHelper.Map<List<OrderGoodsDTO>>(listGoods);
                        dto.Add(itemDTO);
                        //取消订单
                        if (((DateTime)data.add_time).AddMinutes(30) < DateTime.Now && data.pay_status == 0 && data.order_status == 1)
                        {
                            APIHelper.OrderCancel(data.order_id);
                        }
                    });

                    ret.Data = dto;
                    ret.status = true;
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 待付款订单
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<OrderListDTO>> OrderListPrePayGet(string token)
        {
            
            RetInfo<List<OrderListDTO>> ret = new RetInfo<List<OrderListDTO>>();

            try
            {
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                if (user != null)
                {
                    List<OrderListDTO> dto = new List<OrderListDTO>();
                    List<t_order_info> listOrder = OperateContext.EFBLLSession.t_order_infoBLL.GetListByDesc(o =>o.user_id == user.ID && o.order_status == 1 && o.pay_status == 0, o => o.add_time);
                    listOrder.ForEach(data =>
                    {
                        OrderListDTO itemDTO = new OrderListDTO();
                        List<t_order_goods> listGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == data.order_id);
                        itemDTO.order_info = DTOHelper.Map<OrderInfoDTO>(data);
                        itemDTO.order_detail = DTOHelper.Map<List<OrderGoodsDTO>>(listGoods);
                        dto.Add(itemDTO);
                        //取消订单
                        if (((DateTime)data.add_time).AddMinutes(30) < DateTime.Now && data.pay_status == 0 && data.order_status == 1)
                        {
                            APIHelper.OrderCancel(data.order_id);
                        }
                    });

                    

                    ret.Data = dto;
                    ret.status = true;
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 配送中订单
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<OrderListDTO>> OrderListShippingGet(string token)
        {
            RetInfo<List<OrderListDTO>> ret = new RetInfo<List<OrderListDTO>>();

            try
            {
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                if (user != null)
                {
                    List<OrderListDTO> dto = new List<OrderListDTO>();
                    List<t_order_info> listOrder = OperateContext.EFBLLSession.t_order_infoBLL.GetListByDesc(o =>o.user_id == user.ID && o.order_status == 1 && o.pay_status == 1, o => o.add_time);
                    listOrder.ForEach(data =>
                    {
                        OrderListDTO itemDTO = new OrderListDTO();
                        List<t_order_goods> listGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == data.order_id);
                        itemDTO.order_info = DTOHelper.Map<OrderInfoDTO>(data);
                        itemDTO.order_detail = DTOHelper.Map<List<OrderGoodsDTO>>(listGoods);
                        dto.Add(itemDTO);
                    });

                    ret.Data = dto;
                    ret.status = true;
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }

        /// <summary>
        /// 待评价订单
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<OrderListDTO>> OrderListPreCommentGet(string token)
        {
            RetInfo<List<OrderListDTO>> ret = new RetInfo<List<OrderListDTO>>();

            try
            {
                t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                if (user != null)
                {
                    List<OrderListDTO> dto = new List<OrderListDTO>();
                    List<t_order_info> listOrder = OperateContext.EFBLLSession.t_order_infoBLL.GetListByDesc(o =>o.user_id == user.ID && o.order_status == 3 && o.pay_status == 1, o => o.add_time);
                    listOrder.ForEach(data =>
                    {
                        OrderListDTO itemDTO = new OrderListDTO();
                        List<t_order_goods> listGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == data.order_id);
                        itemDTO.order_info = DTOHelper.Map<OrderInfoDTO>(data);
                        itemDTO.order_detail = DTOHelper.Map<List<OrderGoodsDTO>>(listGoods);
                        dto.Add(itemDTO);
                    });

                    ret.Data = dto;
                    ret.status = true;
                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
            }

            return ret;
        }




    }
}
