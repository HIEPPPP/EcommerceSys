using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;

namespace Ecommerce.API.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Product, CreateProductRequestDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();  
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();

            CreateMap<ProductInventory, CreateProductInventoryRequestDto>().ReverseMap();
            CreateMap<ProductInventory, UpdateProductRequestDto>().ForMember(x => x.Quantity, otp => otp.MapFrom(x => x.Quantity)).ReverseMap();
            
            CreateMap<ProductCategory, CreateCategoryRequestDto>().ReverseMap();    
            CreateMap<ProductCategory, UpdateCategoryRequestDto>().ReverseMap();

            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, CreateUpdateDiscountRequestDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUpdateUserRequestDto>().ReverseMap();

            CreateMap<UserAddress, CreateUpdateUAddressRequestDto>().ReverseMap();
            CreateMap<UserAddress, UAddressDto>().ReverseMap(); 

            CreateMap<UserPayment, UPaymentDto>().ReverseMap();
            CreateMap<UserPayment, CreateUPaymentRequestDto>().ReverseMap();
            CreateMap<UserPayment, UpdateUPaymentRequestDto>().ReverseMap();

        }
    }
}
