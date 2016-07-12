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
            Mapper.CreateMap<t_goods, GoodsDTO>().ForMember(dest => dest.goods_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.goods_img)); ;
            Mapper.CreateMap<t_user, UserDTO>();
            Mapper.CreateMap<t_user_address, UserAddressDTO>();
            Mapper.CreateMap<t_banner, BannerDTO>().ForMember(dest => dest.banner_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.banner_img));
            Mapper.CreateMap<t_ad, ADDTO>().ForMember(dest => dest.ad_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.ad_img));
            Mapper.CreateMap<t_category_type, CatTypeDTO>().ForMember(desc => desc.type_img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.type_img));
            Mapper.CreateMap<t_goods_gallery, GoodsGalleryDTO>().ForMember(desc => desc.img, opt => opt.MapFrom(src => ConfigurationHelper.AppSetting("Domain") + src.img));

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
