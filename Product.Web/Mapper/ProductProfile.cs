//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Product.Domain;
//using Product.Web.Dto;

//namespace Product.Web.Mapper
//{
//    public class ProductProfile : Profile
//    {
//        public ProductProfile()
//        {
//            //CreateMap<ProductInfo, ProductDto>().ReverseMap()
//            //         .ForMember(dest => dest.Type,
//            //        opt => opt.MapFrom(s => (int)s.Type)); ;

//            CreateMap<ProductInfo, ProductDto>()
//                .ForMember(destination => destination.Type,
//                 opt => opt.MapFrom(source => Enum.GetName(typeof(ProductType), source.Type)));


//            CreateMap<ProductInfo, ProductInsertDto>()
//                .ForMember(destination => destination.Type,
//                 opt => opt.MapFrom(source => Enum.GetName(typeof(ProductType), source.Type)));

//            CreateMap<ProductDto, ProductInsertDto>()
//              .ForMember(destination => destination.Type,
//               opt => opt.MapFrom(source => Enum.Parse(typeof(ProductType), source.Type, true)));

//            //CreateMap<ProductInfo, ProductDto>()
//            //.ForMember(destination => destination.Types,
//            //                      opt => opt.MapFrom(s => Enum.GetValues(typeof(ProductType)).Cast<string>().ToList()));
//        }
//    }
  
//}
