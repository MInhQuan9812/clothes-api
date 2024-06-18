using AutoMapper;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
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


        }
    }
}
