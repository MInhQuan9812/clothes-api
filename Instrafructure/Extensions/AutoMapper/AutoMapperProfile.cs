using AutoMapper;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Dtos.Product;
using clothes.api.Dtos.Products;
using clothes.api.Dtos.User;
using clothes.api.Instrafructure.Entities;

namespace clothes.api.Instrafructure.Extensions.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<SignUpDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Option, OptionDto>();
            CreateMap<OptionValue, AddValueToOptionDto>();
            CreateMap<OptionValue,OptionValueDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<OptionValue,CreateOptionValueDto >();
            CreateMap<ProductOptionValue, ProductOptionValueResponeDto>();
            CreateMap<OptionValue, OptionValueDto>();

        }
    }
}
