using AutoMapper;
using Base.Entities.Concrete;
using Entities.Concrete;
using Entities.Models.Requests;
using Entities.Models.Responses;

namespace Business.Mapping;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();

        CreateMap<Product, ProductResponse>();
        CreateMap<ProductRequest, Product>();

        CreateMap<CartItemRequest, CartItem>();
        CreateMap<CartItem, CartItemResponse>();

        CreateMap<User, UserResponse>();
        CreateMap<UserRequest, User>();
    }
}
