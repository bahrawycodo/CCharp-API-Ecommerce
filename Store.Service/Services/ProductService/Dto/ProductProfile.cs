using AutoMapper;
using Store.Data.Entities;

namespace Store.Service.Services.ProductService.Dto
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product,ProductDetailsDto>()
                .ForMember(dest=>dest.BrandName,options=>options.MapFrom(src=>src.Brand.Name))
                .ForMember(dest=>dest.TypeName,options=>options.MapFrom(src=>src.Type.Name))
                .ForMember(dest=>dest.PictureUrl,options=>options.MapFrom<ProductUrlResolver>()).ReverseMap();
            CreateMap<ProductType, BrandTypeDetailsDto>();
            CreateMap<ProductBrand, BrandTypeDetailsDto>();
        }
    }
}
