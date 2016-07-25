using AutoMapper;
using Common;
using Model;
using Model.DTOModel;
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
            Mapper.CreateMap<t_goods, GoodsDTO>().ForMember(dest => dest.goods_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.goods_img))
                .ForMember(desc=>desc.goods_number,opt=>opt.MapFrom(src=>src.goods_number - src.goods_lock_number));
            Mapper.CreateMap<t_user, UserDTO>();
            Mapper.CreateMap<t_user_address, UserAddressDTO>();
            Mapper.CreateMap<t_banner, BannerDTO>().ForMember(dest => dest.banner_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.banner_img));
            Mapper.CreateMap<t_ad, ADDTO>().ForMember(dest => dest.ad_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.ad_img));
            Mapper.CreateMap<t_category_type, CatTypeDTO>().ForMember(desc => desc.type_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.type_img));
            Mapper.CreateMap<t_goods_gallery, GoodsGalleryDTO>().ForMember(desc => desc.img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.img));

            //Order
            Mapper.CreateMap<t_order_info, OrderInfoDTO>()
                .ForMember(desc => desc.add_time, opt =>opt.MapFrom(src => src.add_time == null ? "":((DateTime)src.add_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.confirm_time, opt => opt.MapFrom(src =>src.confirm_time == null? "": ((DateTime)src.confirm_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.pay_time, opt => opt.MapFrom(src => src.pay_time == null ? "" : ((DateTime)src.pay_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc => desc.shipping_time, opt => opt.MapFrom(src => src.shipping_time == null? "": ((DateTime)src.shipping_time).ToString("yyyy-MM-dd HH:mm:ss")));
            Mapper.CreateMap<t_order_goods, OrderGoodsDTO>();
            Mapper.CreateMap<t_cart, CartDTO>().ForMember(desc => desc.goods_img, opt => opt.MapFrom(src => src.t_goods.goods_img));
            Mapper.CreateMap<t_comment, CommentDTO>()
                .ForMember(desc=>desc.create_time,opt=>opt.MapFrom(src=>((DateTime)src.create_time).ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(desc=>desc.user_name,opt=>opt.MapFrom(src=>src.t_user.user_name));

            //Coupon
            Mapper.CreateMap<t_user_coupon, UserCouponDTO>()
                .ForMember(desc => desc.begin_time, opt => opt.MapFrom(src => src.begin_time == null ? "" : ((DateTime)src.begin_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.end_time, opt => opt.MapFrom(src => src.end_time == null ? "" : ((DateTime)src.end_time).ToString("yyyy-MM-dd")))
                .ForMember(desc => desc.use_time, opt => opt.MapFrom(src => src.use_time == null ? "" : ((DateTime)src.use_time).ToString("yyyy-MM-dd HH:mm:ss")));


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
