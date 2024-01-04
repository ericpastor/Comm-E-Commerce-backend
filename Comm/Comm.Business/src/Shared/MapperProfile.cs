using AutoMapper;
using Comm.Business.src.DTO;
using Comm.Business.src.DTOs;
using Comm.Core.src.Entities;

namespace Comm.Business.src.Shared
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserReadDto>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => new AddressReadDto { HouseNumber = a.HouseNumber, PostCode = a.PostCode, Street = a.Street })))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => new AvatarReadDto { AvatarUrl = src.Avatar.AvatarUrl }))
                .PreserveReferences();

            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => new Address { HouseNumber = a.HouseNumber, PostCode = a.PostCode, Street = a.Street })))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => new Avatar { AvatarUrl = src.Avatar.AvatarUrl }))
                .PreserveReferences();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses.Select(a => new Address { HouseNumber = a.HouseNumber, PostCode = a.PostCode, Street = a.Street })))
                .ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => new Avatar { AvatarUrl = src.Avatar.AvatarUrl }))
                .PreserveReferences();

            CreateMap<Avatar, AvatarReadDto>();
            CreateMap<AvatarCreateDto, Avatar>();
            CreateMap<AvatarUpdateDto, Avatar>();

            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>();

            CreateMap<Product, ProductReadDto>()
                 .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                 .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images.Select(img => new ImageReadDto { ImageUrl = img.ImageUrl })));

            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Images, map => map.MapFrom(src => src.Images.Select(img => new Image { ImageUrl = img.ImageUrl })))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Images, map => map.MapFrom(src => src.Images.Select(img => new Image { ImageUrl = img.ImageUrl })))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            CreateMap<Image, ImageReadDto>();
            CreateMap<ImageCreateDto, Image>();
            CreateMap<ImageUpdateDto, Image>();

            CreateMap<Category, CategoryReadDto>().ForMember(dest => dest.CategoryImage, opt => opt.MapFrom(src => new CategoryImageReadDto { CategoryImageUrl = src.CategoryImage.CategoryImageUrl }));
            CreateMap<CategoryCreateDto, Category>().ForMember(dest => dest.CategoryImage, opt => opt.MapFrom(src => new CategoryImage { CategoryImageUrl = src.CategoryImage.CategoryImageUrl }));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.CategoryImage, opt => opt.MapFrom(src => new CategoryImage { CategoryImageUrl = src.CategoryImage.CategoryImageUrl }));

            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<OrderUpdateDto, Order>()
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.OrderProducts));

            CreateMap<OrderProduct, OrderProductReadDto>();
            CreateMap<OrderProductCreateDto, OrderProduct>();
            CreateMap<OrderProductUpdateDto, OrderProduct>();

            CreateMap<OrderProductReadDto, OrderProduct>();
        }
    }
}