using AutoMapper;
using Common;
using Model;
using Model.DTOModel;
using Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperCommon
{
    public static partial class DTOHelper
    {
        static DTOHelper()
        {
            Mapper.CreateMap<t_category, CategoryDTO>();
            Mapper.CreateMap<t_goods, GoodsDTO>()
                .ForMember(dest => dest.goods_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.goods_img))
                .ForMember(desc => desc.goods_number, opt => opt.MapFrom(src => src.goods_number - src.goods_lock_number))
                .ForMember(desc => desc.is_sellout, opt => opt.MapFrom(src => (src.goods_number - src.goods_lock_number) <= 0 ? true : false))
                .ForMember(desc => desc.goods_number_virtual, opt => opt.MapFrom(src => src.goods_number * src.goods_multiple));
            Mapper.CreateMap<t_user, UserDTO>();
            Mapper.CreateMap<t_user_address, UserAddressDTO>();
            Mapper.CreateMap<t_banner, BannerDTO>().ForMember(dest => dest.banner_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.banner_img));
            Mapper.CreateMap<t_ad, ADDTO>().ForMember(dest => dest.ad_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.ad_img));
            Mapper.CreateMap<t_category_type, CatTypeDTO>().ForMember(desc => desc.type_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.type_img));
            Mapper.CreateMap<t_goods_gallery, GoodsGalleryDTO>().ForMember(desc => desc.img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.img));

            //Order
            Mapper.CreateMap<t_order_info, OrderInfoDTO>()
                .ForMember(desc => desc.add_time, opt => opt.MapFrom(src => src.add_time == null ? "" : ((DateTime)src.add_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.confirm_time, opt => opt.MapFrom(src => src.confirm_time == null ? "" : ((DateTime)src.confirm_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.pay_time, opt => opt.MapFrom(src => src.pay_time == null ? "" : ((DateTime)src.pay_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.pay_end_time, opt => opt.MapFrom(src => src.add_time == null ? "" : ((DateTime)src.add_time).AddMinutes(30).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.shipping_time, opt => opt.MapFrom(src => src.shipping_time == null ? "" : ((DateTime)src.shipping_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.order_status_msg, opt => opt.MapFrom(src => ContentHelper.GetOrderStatusMsg(src.order_status, src.pay_status)));
            Mapper.CreateMap<t_order_goods, OrderGoodsDTO>()
                .ForMember(desc => desc.goods_img, opt => opt.MapFrom(src => ContentHelper.GetGoodsImg(src.goods_id)));
            Mapper.CreateMap<t_cart, CartDTO>()
                .ForMember(desc => desc.goods_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.t_goods.goods_img))
                .ForMember(desc => desc.repertory_number, opt => opt.MapFrom(src => (src.t_goods.goods_number - src.t_goods.goods_lock_number)))
                .ForMember(desc => desc.is_activity, opt => opt.MapFrom(src => src.t_goods.is_activity));
            Mapper.CreateMap<t_comment, CommentDTO>()
                .ForMember(desc=>desc.create_time,opt=>opt.MapFrom(src=>((DateTime)src.create_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc=>desc.user_name,opt=>opt.MapFrom(src=>string.IsNullOrEmpty(src.t_user.user_name) == true ? ContentHelper.GetHidePhone(src.t_user.user_phone): src.t_user.user_name))
                .ForMember(desc=>desc.user_img,opt=>opt.MapFrom(src=>src.t_user.user_img == null?"":ConfigurationHelper.AppSetting("Domain") + src.t_user.user_img))
                .ForMember(desc=>desc.comment_imgs,opt=>opt.MapFrom(src=>ContentHelper.GetCommentImgs(src.comment_imgs)));

            //Coupon
            Mapper.CreateMap<t_user_coupon, UserCouponDTO>()
                .ForMember(desc => desc.begin_time, opt => opt.MapFrom(src => src.begin_time == null ? "" : ((DateTime)src.begin_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.end_time, opt => opt.MapFrom(src => src.end_time == null ? "" : ((DateTime)src.end_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.use_time, opt => opt.MapFrom(src => src.use_time == null ? "" : ((DateTime)src.use_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.coupon_img, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.coupon_img) ? "" : ConfigurationHelper.AppSetting("Domain") + src.coupon_img)); ;

            //Goods
            Mapper.CreateMap<t_wechat_seller, WechatSellerDTO>()
                .ForMember(desc => desc.we_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.we_img));

            //VM
            Mapper.CreateMap<t_order_goods, OrderGoodsViewModel>();
            Mapper.CreateMap<t_coupon, CouponVM>()
                .ForMember(desc => desc.coupon_img, opt => opt.MapFrom(src => string.IsNullOrEmpty(src.coupon_img) ? "" : ConfigurationHelper.AppSetting("Domain") + src.coupon_img));
            Mapper.CreateMap<t_user_coupon, UserCouponVM>()
                .ForMember(desc => desc.begin_time, opt => opt.MapFrom(src => src.begin_time == null ? "" : ((DateTime)src.begin_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.end_time, opt => opt.MapFrom(src => src.end_time == null ? "" : ((DateTime)src.end_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.coupon_name, opt => opt.MapFrom(src => ContentHelper.GetCouponName(src.coupon_id)));
            Mapper.CreateMap<t_goods, OrderStatisticsVM>()
                .ForMember(desc => desc.order_goods_count, opt => opt.MapFrom(src => APIHelper.OrderGoodsCount(src.goods_id)));


        }

        static void Visa<T1, T2>()
        {
            Mapper.CreateMap<T1, T2>();
            Mapper.CreateMap<T2, T1>();
        }

        /// <summary>
        /// DTO转换
        /// </summary>
        /// <typeparam name="ToT"></typeparam>
        /// <param name="org"></param>
        /// <returns></returns>
        public static ToT Map<ToT>(object org)
        {
            return Mapper.Map<ToT>(org);
        }

        /// <summary>
        /// DTO集合转换
        /// </summary>
        /// <typeparam name="ToT"></typeparam>
        /// <param name="org"></param>
        /// <returns></returns>
        public static List<ToT> MapList<ToT>(object org)
        {
            return Mapper.Map<List<ToT>>(org);
        }

    }
}
