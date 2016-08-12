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
    public class OrderController : ApiController
    {

        /// <summary>
        /// 购物车列表By
        /// </summary>
        /// <param name="token">用户ID</param>
        /// <param name="cart_type">购物车类型(0:普通,1:预购)</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<CartListDTO> CartListBy(int cart_type,string token = "")
        {
            RetInfo<CartListDTO> ret = new RetInfo<CartListDTO>();

            try
            {
                //if (APIHelper.IsLogin(token))
                //{
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u=>u.token == token.Trim());
                    CartListDTO dto = new CartListDTO();
                    if (user != null)
                    {
                        List<t_cart> listCart = OperateContext.EFBLLSession.t_cartBLL.GetListByDesc(c => c.user_id == user.ID && c.cart_type == cart_type, c => c.cart_id);
                        listCart.ForEach(item => { 
                            t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g=>g.goods_id == item.goods_id);
                            if(goods != null)
                            {
                                if((goods.goods_number - goods.goods_lock_number) < item.goods_number)
                                {
                                    item.goods_number = (goods.goods_number - goods.goods_lock_number);
                                    OperateContext.EFBLLSession.t_cartBLL.Modify(item);
                                }
                            }
                        });
                        listCart = OperateContext.EFBLLSession.t_cartBLL.GetListByDesc(c => c.user_id == user.ID && c.cart_type == cart_type, c => c.cart_id);
                        dto.cart = DTOHelper.Map<List<CartDTO>>(listCart);
                    }

                    List<t_goods> listGoods = OperateContext.EFBLLSession.t_goodsBLL.GetPageListDesc(1, 6, g => g.is_del == false && g.is_on_sale == true, g => g.sort);
                    dto.goods = DTOHelper.Map<List<GoodsDTO>>(listGoods);
                    ret.Data = dto;
                    ret.status = true;

                    //else
                    //{
                    //    ret.msg = CommonBasicMsg.VoidUser;
                    //}
                //}
                //else
                //{
                //    ret.msg = Message.NoLogin;
                //}
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="obj">{"token":"用户Token","goods_id":商品ID,"number":加入购物车数量}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> CartAdd(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                int goods_id = obj.goods_id;
                int number = obj.number;
                //int iGoods_id = 0;
                //if (int.TryParse(goods_id, out iGoods_id))
                //{
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(u => u.goods_id == goods_id && u.is_del == false && u.is_on_sale == true);
                        if (goods != null)
                        {
                            if ((goods.goods_number - goods.goods_lock_number) >= number)
                            {
                                t_cart editCart = OperateContext.EFBLLSession.t_cartBLL.GetModelBy(c => c.user_id == user.ID && c.goods_id == goods.goods_id);

                                if (editCart != null)
                                {
                                    if ((goods.goods_number - goods.goods_lock_number - editCart.goods_number) >= number)
                                    {
                                        editCart.goods_number = editCart.goods_number + number;

                                        if (OperateContext.EFBLLSession.t_cartBLL.Modify(editCart))
                                        {
                                            ret.status = true;
                                            ret.msg = CommonBasicMsg.EditSuc;
                                            //购物车数量
                                            ret.recordCount = editCart.goods_number;
                                        }
                                        else
                                        {
                                            ret.msg = CommonBasicMsg.EditFail;
                                        }
                                    }
                                    else
                                    {
                                        ret.msg = "库存不足";
                                    }
                                    
                                }
                                else
                                {
                                    t_cart addCart = new t_cart()
                                    {
                                        cart_type = goods.is_pre_sale == true ? (byte)1 : (byte)0,
                                        user_id = user.ID,
                                        goods_id = goods.goods_id,
                                        goods_name = goods.goods_name,
                                        market_price = goods.goods_price,
                                        goods_price = goods.goods_price,
                                        goods_number = number
                                    };

                                    if (OperateContext.EFBLLSession.t_cartBLL.Add(addCart))
                                    {
                                        ret.status = true;
                                        ret.msg = CommonBasicMsg.AddSuc;
                                        //购物车数量
                                        ret.recordCount = addCart.goods_number;
                                    }
                                    else
                                    {
                                        ret.msg = CommonBasicMsg.AddFail;
                                    }
                                }
                            }
                            else
                            {
                                ret.msg = "库存不足";
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidGoods;
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
                //}
                //else
                //{
                //    ret.msg = CommonBasicMsg.VoidID;
                //}
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }


            return ret;
        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="obj">{"token":"用户Token","goods_id":商品ID}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> CartDelete(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            string token = obj.token;
            int goods_id = obj.goods_id;

            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
            if (user != null)
            {
                if (OperateContext.EFBLLSession.t_cartBLL.GetCountBy(c => c.user_id == user.ID && c.goods_id == goods_id) > 0)
                {
                    t_cart cartModel = OperateContext.EFBLLSession.t_cartBLL.GetModelBy(c => c.user_id == user.ID && c.goods_id == goods_id);
                    if (cartModel != null)
                    {
                        t_goods goodsModel = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(c => c.goods_id == goods_id);
                        if (goodsModel != null)
                        {
                            if (OperateContext.EFBLLSession.t_cartBLL.DeleteBy(c => c.user_id == user.ID && c.goods_id == goods_id))
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
                            ret.msg = CommonBasicMsg.VoidGoods;
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidGoods;
                    }
                }
                else
                {
                    ret.msg = "购物车不存在此商品或无权限删除";
                }
            }
            else
            {
                ret.msg = CommonBasicMsg.NoLogin;
            }


            return ret;
        }

        /// <summary>
        /// 订单用户地址选择
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<UserAddressDTO>> OrderAddressListGet(string token)
        {
            RetInfo<List<UserAddressDTO>> ret = new RetInfo<List<UserAddressDTO>>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        List<t_user_address> listAddress = OperateContext.EFBLLSession.t_user_addressBLL.GetListByDesc(a => a.user_id == user.ID, a => a.address_id);
                        ret.Data = DTOHelper.Map<List<UserAddressDTO>>(listAddress);
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
        /// 订单检查（购物车）
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="cart_type">购物车类型0(普通)1(预购)</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CartDTO>> OrderCartCheck(string token, int cart_type)
        {
            RetInfo<List<CartDTO>> ret = new RetInfo<List<CartDTO>>();

            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
            if (user != null)
            {
                bool flag = true;
                List<t_cart> listCart = OperateContext.EFBLLSession.t_cartBLL.GetListByDesc(c => c.user_id == user.ID && c.cart_type == cart_type, c => c.cart_id);
                listCart.ForEach(item =>
                {
                    t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == item.goods_id);
                    if (goods != null)
                    {
                        if ((goods.goods_number - goods.goods_lock_number) < item.goods_number)
                        {
                            flag = false;
                            item.goods_number = (goods.goods_number - goods.goods_lock_number);
                            OperateContext.EFBLLSession.t_cartBLL.Modify(item);
                        }
                    }
                });
                //是否生成
                if (flag == false)
                {
                    listCart = OperateContext.EFBLLSession.t_cartBLL.GetListByDesc(c => c.user_id == user.ID && c.cart_type == cart_type, c => c.cart_id);
                    ret.Data = DTOHelper.Map<List<CartDTO>>(listCart);
                }
                else
                {
                    ret.status = true;
                }
            }
            else
            {
                ret.msg = CommonBasicMsg.NoLogin;
            }

            return ret;
        }

        /// <summary>
        /// 购物车订单确认
        /// </summary>
        /// <param name="obj">{"token":"用户Token","cart_type":购物车类型0(普通)1(预购),"address_id":用户地址ID,"uc_id":用户优惠券ID,无则0,"expect_shipping_time":"期望配送时间"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<OrderDetailDTO> OrderCartConfirm(dynamic obj)
        {
            RetInfo<OrderDetailDTO> ret = new RetInfo<OrderDetailDTO>();

            try
            {
                string token = obj.token;
                int cart_type = obj.cart_type;
                int address_id = obj.address_id;
                int uc_id = obj.uc_id;
                string expect_shipping_time = obj.expect_shipping_time;

                if (APIHelper.IsLogin(token))
                {
                    t_user_address address = OperateContext.EFBLLSession.t_user_addressBLL.GetModelBy(a => a.address_id == address_id);
                    if (address != null)
                    {
                        t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                        if (user != null)
                        {
                            //购物车
                            List<t_cart> listCart = OperateContext.EFBLLSession.t_cartBLL.GetListBy(c => c.user_id == user.ID && c.cart_type == cart_type && (c.t_goods.goods_number - c.t_goods.goods_lock_number) > 0);
                            if (listCart.Count > 0)
                            {

                                //a Order_Base_Info
                                t_order_info order = new t_order_info()
                                {
                                    order_type = (byte)cart_type,
                                    order_sn = DateTime.Now.ToString("yyyyMMddHHmmssfff") + user.ID.ToString(),
                                    user_id = user.ID,
                                    order_status = 1,
                                    shipping_status = 0,
                                    pay_status = 0,
                                    consignee = address.receipt_person,
                                    mobile = address.receipt_phone,
                                    area = address.area,
                                    building = address.building,
                                    room_num = address.room_num,
                                    address = address.address,

                                    uc_id = 0, 
                                    uc_amount = 0,
                                    expect_shipping_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + expect_shipping_time,

                                    postscript = "",
                                    goods_amount = 0,
                                    order_amount = 0,
                                    money_paid = 0,
                                    add_time = DateTime.Now,
                                    confirm_time = DateTime.Now
                                };

                                //b Order_Goods
                                List<t_order_goods> listOrderGoods = new List<t_order_goods>();
                                listCart.ForEach(data =>
                                {
                                    if (data.t_goods.is_on_sale == true)
                                    {
                                        if ((data.t_goods.goods_number - data.t_goods.goods_lock_number) > 0)
                                        {
                                            t_order_goods orderGoods = new t_order_goods()
                                            {
                                                //order_id = order.order_id,
                                                goods_id = data.goods_id,
                                                goods_name = data.t_goods.goods_name,
                                                goods_number = (data.t_goods.goods_number - data.t_goods.goods_lock_number) > data.goods_number ? data.goods_number : (data.t_goods.goods_number - data.t_goods.goods_lock_number),
                                                market_price = data.market_price,
                                                goods_price = data.goods_price
                                            };
                                            listOrderGoods.Add(orderGoods);
                                        }
                                    }
                                });
                                //有效数量
                                if (listOrderGoods.Count > 0)
                                {
                                    order.goods_amount = listOrderGoods.Sum(g => g.goods_number * g.goods_price);
                                    order.order_amount = order.goods_amount;
                                    //优惠券处理
                                    //pre User_Coupon
                                    t_user_coupon userCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetModelBy(u => u.uc_id == uc_id);
                                    if (userCoupon != null)
                                    {
                                        if (userCoupon.condition_amount <= order.goods_amount)
                                        {
                                            order.uc_id = userCoupon.uc_id;
                                            order.uc_amount = userCoupon.coupon_amount;
                                            order.order_amount = (order.goods_amount - order.uc_amount) <= 0 ? 0 : (order.goods_amount - order.uc_amount);//listOrderGoods.Sum(g => g.goods_number * g.goods_price);
                                            //已使用
                                            userCoupon.is_use = true;
                                            userCoupon.use_time = DateTime.Now;
                                            OperateContext.EFBLLSession.t_user_couponBLL.Modify(userCoupon);
                                        }
                                    }

                                    //c to SQL
                                    if (OperateContext.EFBLLSession.t_order_infoBLL.Add(order))
                                    {
                                        listOrderGoods.ForEach(data =>
                                        {
                                            data.order_id = order.order_id;
                                            OperateContext.EFBLLSession.t_order_goodsBLL.Add(data);
                                            // goods lock_number update
                                            t_goods editGoods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == data.goods_id);
                                            if (editGoods != null)
                                            {
                                                editGoods.goods_lock_number += data.goods_number;
                                                //editGoods.goods_number = editGoods.goods_number - editGoods.goods_lock_number;
                                                OperateContext.EFBLLSession.t_goodsBLL.Modify(editGoods);
                                            }

                                        });
                                        //delete cart
                                        OperateContext.EFBLLSession.t_cartBLL.DeleteBy(c => c.user_id == user.ID && c.cart_type == cart_type);

                                        ret.status = true;
                                        ret.msg = Message.OrderCreateSuc;
                                        //result data
                                        OrderDetailDTO dto = new OrderDetailDTO();
                                        dto.order_info = DTOHelper.Map<OrderInfoDTO>(order);
                                        dto.order_goods = DTOHelper.Map<List<OrderGoodsDTO>>(listOrderGoods);
                                        ret.Data = dto;

                                    }
                                    else
                                    {
                                        ret.msg = Message.OrderCreateFail;
                                    }
                                }
                                else
                                {
                                    ret.msg = "全部售罄";
                                }
                            }
                            else
                            {
                                ret.msg = "无有效商品";
                            }

                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidUser;
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.VoidAddress;
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
        /// 订单检查（直接购买）
        /// </summary>
        /// <param name="token">用户token</param>
        /// <param name="goods_id">商品ID</param>
        /// <param name="number">数量</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<CartDTO>> OrderCheck(string token, int goods_id, int number)
        {
            RetInfo<List<CartDTO>> ret = new RetInfo<List<CartDTO>>();

            t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
            if (user != null)
            {
                bool flag = true;
               
                t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == goods_id);
                if (goods != null)
                {
                    if ((goods.goods_number - goods.goods_lock_number) < number)
                    {
                        CartDTO dto = new CartDTO();
                        dto.cart_type = (byte)(goods.is_pre_sale == true ? 1:0);
                        dto.user_id = user.ID;
                        dto.goods_id = goods.goods_id;
                        dto.goods_name = goods.goods_name;
                        dto.goods_img = ConfigurationHelper.AppSetting("Domain") + goods.goods_img;
                        dto.goods_price = (decimal)goods.goods_price;
                        dto.goods_number = (int)(goods.goods_number - goods.goods_lock_number);
                        ret.Data.Add(dto) ;

                        flag = false;
                    }
                }

                //是否生成
                if (flag == false)
                {
                    
                }
                else
                {
                    ret.status = true;
                }
            }
            else
            {
                ret.msg = CommonBasicMsg.NoLogin;
            }

            return ret;
        }

        /// <summary>
        /// 直接购买订单确认
        /// </summary>
        /// <param name="obj">{"token":"用户Token","goods_id":商品ID,"order_type":购物车类型0(普通)1(预购),"number":购买数量,"address_id":用户地址ID,"uc_id":用户优惠券ID,无则0,"expect_shipping_time":"期望配送时间"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<OrderDetailDTO> OrderConfirm(dynamic obj)
        {
            RetInfo<OrderDetailDTO> ret = new RetInfo<OrderDetailDTO>();

            try
            {
                string token = obj.token;
                int number = obj.number;
                int goods_id = obj.goods_id;
                int order_type = obj.order_type;
                int address_id = obj.address_id;
                int uc_id = obj.uc_id;
                string expect_shipping_time = obj.expect_shipping_time;


                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == goods_id && g.is_on_sale == true);
                        if (goods != null)
                        {
                            if ((goods.goods_number - goods.goods_lock_number) >= number)
                            {
                                t_user_address address = OperateContext.EFBLLSession.t_user_addressBLL.GetModelBy(a => a.address_id == address_id);
                                if (address != null)
                                {
                                    //a order_info
                                    t_order_info order = new t_order_info()
                                    {
                                        order_type = (byte)order_type,
                                        order_sn = DateTime.Now.ToString("yyyyMMddHHmmssfff") + user.ID.ToString(),
                                        user_id = user.ID,
                                        order_status = 1,
                                        shipping_status = 0,
                                        pay_status = 0,
                                        consignee = address.receipt_person,
                                        mobile = address.receipt_phone,
                                        area = address.area,
                                        building = address.building,
                                        room_num = address.room_num,
                                        address = address.address,

                                        uc_id = 0,
                                        uc_amount = 0,
                                        expect_shipping_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + expect_shipping_time,

                                        postscript = "",
                                        goods_amount = 0,
                                        order_amount = 0,
                                        money_paid = 0,
                                        add_time = DateTime.Now,
                                        confirm_time = DateTime.Now
                                    };
                                    //b order_goods
                                    t_order_goods order_goods = new t_order_goods()
                                    {
                                        goods_id = goods.goods_id,
                                        goods_name = goods.goods_name,
                                        goods_number = (goods.goods_number - goods.goods_lock_number) > number ? number : (goods.goods_number - goods.goods_lock_number),
                                        market_price = goods.goods_price,
                                        goods_price = goods.goods_price
                                    };
                                    order.goods_amount = order_goods.goods_number * order_goods.goods_price;
                                    order.order_amount = order.goods_amount;
                                    //优惠券处理
                                    t_user_coupon userCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetModelBy(u => u.uc_id == uc_id);
                                    if (userCoupon != null)
                                    {
                                        if (userCoupon.condition_amount <= order.goods_amount)
                                        {
                                            order.uc_id = userCoupon.uc_id;
                                            order.uc_amount = userCoupon.coupon_amount;
                                            order.order_amount = (order.goods_amount - order.uc_amount) <= 0 ? 0 : (order.goods_amount - order.uc_amount);//listOrderGoods.Sum(g => g.goods_number * g.goods_price);
                                            //已使用
                                            userCoupon.is_use = true;
                                            userCoupon.use_time = DateTime.Now;
                                            OperateContext.EFBLLSession.t_user_couponBLL.Modify(userCoupon);
                                        }
                                    }

                                    //c goods lock_number
                                    goods.goods_lock_number += order_goods.goods_number;
                                    //goods.goods_number = goods.goods_number - order_goods.goods_number;
                                    //d to SQL
                                    if (OperateContext.EFBLLSession.t_order_infoBLL.Add(order))
                                    {
                                        order_goods.order_id = order.order_id;
                                        OperateContext.EFBLLSession.t_order_goodsBLL.Add(order_goods);
                                        OperateContext.EFBLLSession.t_goodsBLL.Modify(goods);

                                        //retult
                                        ret.msg = Message.OrderCreateSuc;
                                        ret.status = true;

                                        //return data
                                        OrderDetailDTO dto = new OrderDetailDTO();
                                        dto.order_goods = DTOHelper.Map<List<OrderGoodsDTO>>(new List<t_order_goods>() { order_goods });
                                        dto.order_info = DTOHelper.Map<OrderInfoDTO>(order);
                                        ret.Data = dto;
                                    }
                                    else
                                    {
                                        ret.msg = Message.OrderCreateFail;
                                    }

                                }
                                else
                                {
                                    ret.msg = CommonBasicMsg.VoidAddress;
                                }
                            }
                            else
                            {
                                ret.msg = "库存不足";
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidGoods;
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
        /// 用户有效优惠券
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <param name="type">优惠券类型（0：及时送达，1：预购，2：全部）</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<List<UserCouponDTO>> UserCouponsGet(string token,int type = 2)
        {
            RetInfo<List<UserCouponDTO>> ret = new RetInfo<List<UserCouponDTO>>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    if (user != null)
                    {
                        List<t_user_coupon> listCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetListByDesc(a => a.user_id == user.ID && a.is_use == false, a => a.end_time);
                        if (listCoupon.Count > 0)
                        {
                            //i 及时送达
                            if (type == 0)
                            {
                                ret.Data = DTOHelper.Map<List<UserCouponDTO>>(listCoupon.Where(c => c.coupon_type == 0));
                            }
                            //ii 预购
                            else if (type == 1)
                            {
                                ret.Data = DTOHelper.Map<List<UserCouponDTO>>(listCoupon.Where(c => c.coupon_type == 1));
                            }
                            //iii 全部
                            else if (type == 2)
                            {
                                ret.Data = DTOHelper.Map<List<UserCouponDTO>>(listCoupon);
                            }
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
        /// 取消订单
        /// </summary>
        /// <param name="obj">{"token":"用户Token","order_id":订单ID}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> OrderCancel(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                int order_id = obj.order_id;

                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token.Trim());
                    t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_id == order_id && o.order_status == 1 && o.pay_status == 0);
                    if (order != null)
                    {
                        if (order.user_id == user.ID)
                        {
                            //1.0 锁定库存
                            List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == order.order_id);
                            listOrderGoods.ForEach(item =>
                            {
                                t_goods goods = OperateContext.EFBLLSession.t_goodsBLL.GetModelBy(g => g.goods_id == item.goods_id);
                                if (goods != null)
                                {
                                    goods.goods_lock_number = (goods.goods_lock_number - item.goods_number) >= 0 ? (goods.goods_lock_number - item.goods_number) : 0;
                                    OperateContext.EFBLLSession.t_goodsBLL.Modify(goods);
                                }
                            });
                            //2.0 订单状态
                            order.order_status = 2;
                            order.cancel_time = DateTime.Now;
                            OperateContext.EFBLLSession.t_order_infoBLL.Modify(order);
                            //3.0 优惠券
                            if (order.uc_id > 0)
                            {
                                t_user_coupon userCoupon = OperateContext.EFBLLSession.t_user_couponBLL.GetModelBy(u => u.uc_id == order.uc_id);
                                userCoupon.is_use = false;
                                userCoupon.use_time = null;
                                OperateContext.EFBLLSession.t_user_couponBLL.Modify(userCoupon);
                            }

                            ret.msg = CommonBasicMsg.OrderCancelSuc;
                            ret.status = true;
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.NoAccess;// "无权限取消";
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.OrderCancelErr;
                    }

                }
                else
                {
                    ret.msg = CommonBasicMsg.NoLogin;
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
        /// 获得订单详情根据订单ID
        /// </summary>
        /// <param name="token">用户Token</param>
        /// <param name="order_id">订单ID</param>
        /// <returns></returns>
        [HttpGet]
        public RetInfo<OrderDetailDTO> OrderDetailBy(string token,int order_id)
        {
            RetInfo<OrderDetailDTO> ret = new RetInfo<OrderDetailDTO>();

            try
            {
                if (APIHelper.IsLogin(token))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_id == order_id);
                        if (order != null)
                        {
                            List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == order.order_id);

                            OrderDetailDTO dto = new OrderDetailDTO();
                            dto.order_info = DTOHelper.Map<OrderInfoDTO>(order);
                            dto.order_goods = DTOHelper.Map<List<OrderGoodsDTO>>(listOrderGoods);

                            ret.status = true;
                            ret.Data = dto;
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.VoidOrder;
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
        /// 订单评论发表
        /// </summary>
        /// <param name="obj">{"token":"用户token","order_id":订单ID,"comment":"评价","comment_imgs":"评论图集逗号分隔"}</param>
        /// <returns></returns>
        [HttpPost]
        public RetInfo<object> OrderCommentSumit(dynamic obj)
        {
            RetInfo<object> ret = new RetInfo<object>();

            try
            {
                string token = obj.token;
                int order_id = obj.order_id;
                string comment = obj.comment;
                string comment_imgs = obj.comment_imgs;

                if (!string.IsNullOrWhiteSpace(comment))
                {
                    t_user user = OperateContext.EFBLLSession.t_userBLL.GetModelBy(u => u.token == token);
                    if (user != null)
                    {
                        t_order_info order = OperateContext.EFBLLSession.t_order_infoBLL.GetModelBy(o => o.order_id == order_id && o.order_status == 3 && o.pay_status == 1);
                        if (order != null)
                        {
                            if (order.user_id == user.ID)
                            {
                                List<t_order_goods> listOrderGoods = OperateContext.EFBLLSession.t_order_goodsBLL.GetListBy(o => o.order_id == order.order_id);
                                listOrderGoods.ForEach(item =>
                                {
                                    t_comment addComment = new t_comment()
                                    {
                                        user_id = user.ID,
                                        order_id = order.order_id,
                                        goods_id = item.goods_id,
                                        comment = comment,
                                        comment_imgs = comment_imgs,
                                        is_del = false,
                                        create_time = DateTime.Now
                                    };
                                    OperateContext.EFBLLSession.t_commentBLL.Add(addComment);
                                });
                                //Finished
                                order.order_status = 4;
                                order.pay_status = 1;
                                if (OperateContext.EFBLLSession.t_order_infoBLL.Modify(order))
                                {
                                    ret.status = true;
                                    ret.msg = CommonBasicMsg.OrderCommentSuc;
                                }
                                else
                                {
                                    ret.msg = "评论失败";
                                }
                            }
                            else
                            {
                                ret.msg = CommonBasicMsg.NoAccess;
                            }
                        }
                        else
                        {
                            ret.msg = CommonBasicMsg.OrderCommentErr;
                        }
                    }
                    else
                    {
                        ret.msg = CommonBasicMsg.NoLogin;
                    }
                }
                else
                {
                    ret.msg = CommonBasicMsg.OrderCommentVoid;
                }
            }
            catch (Exception ex)
            {
                ret.msg = ex.ToString();
                Logger.WriteExceptionLog(ex);
            }

            return ret;
        }








    }
}
