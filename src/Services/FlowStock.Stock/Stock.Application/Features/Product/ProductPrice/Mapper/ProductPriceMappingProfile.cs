using AutoMapper;
using Stock.Domain;

namespace Stock.Application;

public sealed class ProductPriceMappingProfile : Profile
{
    public ProductPriceMappingProfile()
    {
        CreateMap<ProductPrice, ProductPriceDto>()
            .ForMember(p=> p.ProductName, dest=> dest.MapFrom(src=> src.Product.Name))
            .ForMember(p=> p.ProductCode, dest=> dest.MapFrom(src=> src.Product.Code));

        CreateMap<ProductPrice, ProductPriceAllInfoDto>()
            .ForMember(p=> p.ProductName, dest=> dest.MapFrom(src=> src.Product.Name))
            .ForMember(p=> p.ProductCode, dest=> dest.MapFrom(src=> src.Product.Code))
            .ForMember(p=> p.IsActive, dest=> dest.MapFrom(src=> src.IsActive ? "Active" : "Expired"));
            


    }
}
