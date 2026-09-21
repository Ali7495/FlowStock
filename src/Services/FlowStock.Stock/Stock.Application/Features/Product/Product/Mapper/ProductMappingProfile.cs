using AutoMapper;
using Stock.Application;
using Stock.Domain;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}