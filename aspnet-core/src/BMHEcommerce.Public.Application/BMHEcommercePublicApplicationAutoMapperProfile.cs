using AutoMapper;
using BMHEcommerce.Manufacturers;
using BMHEcommerce.Orders;
using BMHEcommerce.ProductAttributes;
using BMHEcommerce.ProductCategories;
using BMHEcommerce.Products;
using BMHEcommerce.Public.Catalog.Manufacturers;
using BMHEcommerce.Public.Catalog.ProductAttributes;
using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.Public.Catalog.Products;
using BMHEcommerce.Public.Orders;
using Volo.Abp.Identity;

namespace BMHEcommerce.Public;

public class BMHEcommercePublicApplicationAutoMapperProfile : Profile
{
    public BMHEcommercePublicApplicationAutoMapperProfile()
    {
        //ProductCategories
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();

        //Products
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();

        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();

        //Product attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();

        //User
        //CreateMap<IdentityUser, UserDto>();
        //CreateMap<IdentityUser, UserInListDto>();

        //Order
        CreateMap<Order, OrderDto>();
    }
}
