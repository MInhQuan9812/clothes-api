using AutoMapper;
using clothes.api.Dtos.Carts;
using clothes.api.Dtos.Category;
using clothes.api.Dtos.Options;
using clothes.api.Dtos.Payments;
using clothes.api.Dtos.Product;
using clothes.api.Dtos.Products;
using clothes.api.Dtos.Promotions;
using clothes.api.Dtos.User;
using clothes.api.Dtos.Wishlist;
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
            CreateMap<CartItem, CartItemDto>();
            CreateMap<ProductVariant, ProductVarientDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<Product, ProductInfoDto>();
            CreateMap<ProductVariant, ProductSkuDto>();
            CreateMap<Option, ProductOptionDto>();
            CreateMap<VariantValue, VariantValueDto>();
            CreateMap<OptionValue, OptionValueCartItemDto>();
            CreateMap<OptionValue, OptionValueNameFieldDto>();
            CreateMap<Payment, CreatePaymentDto>();
            CreateMap<Payment, PaymentDto>();
            CreateMap<Promotion, PromotionDto>();
            CreateMap<PromotionType, PromotionTypeDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<Wishlist, WishlistDto>();
            CreateMap<Wishlist, AddToWishListDto>();
            CreateMap<WishlistItem, WishlistItemDto>();

        }
    }
}
