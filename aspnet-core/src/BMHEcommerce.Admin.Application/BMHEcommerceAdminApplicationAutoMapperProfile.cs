using AutoMapper;
using BMHEcommerce.Admin.Catalog.Manufacturers;
using BMHEcommerce.Admin.Catalog.ProductAttributes;
using BMHEcommerce.Admin.Catalog.ProductCategories;
using BMHEcommerce.Admin.Catalog.Products;
using BMHEcommerce.Admin.System.Roles;
using BMHEcommerce.Admin.System.User;
using BMHEcommerce.Manufacturers;
using BMHEcommerce.ProductAttributes;
using BMHEcommerce.ProductCategories;
using BMHEcommerce.Products;
using BMHEcommerce.Roles;
using Volo.Abp.Identity;

namespace BMHEcommerce.Admin;

public class BMHEcommerceAdminApplicationAutoMapperProfile : Profile
{
    public BMHEcommerceAdminApplicationAutoMapperProfile()
    {
        //ProductCategories
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, ProductCategoryInListDto>();
        CreateMap<CreateUpdateProductCategoryDto, ProductCategory>();

        //Products
        CreateMap<Product, ProductDto>();
        CreateMap<Product, ProductInListDto>();
        CreateMap<CreateUpdateProductDto, Product>();

        //Manufacturer
        CreateMap<Manufacturer, ManufacturerDto>();
        CreateMap<Manufacturer, ManufacturerInListDto>();
        CreateMap<CreateUpdateManufacturerDto, Manufacturer>();

        //Product attribute
        CreateMap<ProductAttribute, ProductAttributeDto>();
        CreateMap<ProductAttribute, ProductAttributeInListDto>();
        CreateMap<CreateUpdateProductAttributeDto, ProductAttribute>();

        //Role
        CreateMap<IdentityRole, RoleDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<IdentityRole, RoleInListDto>().ForMember(x => x.Description,
            map => map.MapFrom(x => x.ExtraProperties.ContainsKey(RoleConsts.DescriptionFieldName)
            ?
            x.ExtraProperties[RoleConsts.DescriptionFieldName]
            :
            null));
        CreateMap<CreateUpdateRoleDto, IdentityRole>();

        //User
        CreateMap<IdentityUser, UserDto>();
        CreateMap<IdentityUser, UserInListDto>();
    }
}
