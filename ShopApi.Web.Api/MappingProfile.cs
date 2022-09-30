using AutoMapper;
using ShopApi.Entities.DataTransferObjects;
using ShopApi.Entities.Models;

namespace ShopApi.Web.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(c => c.Description,
                opt => opt.MapFrom(x => string.Join(' ', x.Description, x.Manufacturer)));
        CreateMap<Customer, CustomerDto>();
        CreateMap<ProductForCreationDto, Product>();
        CreateMap<CustomerForCreationDto, Customer>();
        CreateMap<CustomerForUpdateDto, Customer>().ReverseMap(); 
        CreateMap<ProductForUpdateDto, Product>();
    }
}