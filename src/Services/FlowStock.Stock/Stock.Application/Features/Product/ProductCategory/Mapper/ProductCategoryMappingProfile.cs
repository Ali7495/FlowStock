using AutoMapper;
using Stock.Domain;

namespace Stock.Application;

public class ProductCategoryMappingProfile : Profile
{
    public ProductCategoryMappingProfile()
    {
        CreateMap<ProductCategory, ProductCategoryDto>();
    }
}
