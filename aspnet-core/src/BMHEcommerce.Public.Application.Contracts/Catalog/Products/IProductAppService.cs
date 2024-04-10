using BMHEcommerce.Public.Catalog.Products;
using BMHEcommerce.Public.Catalog.Products.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public.Catalog.Products
{
    public interface IProductsAppService : IReadOnlyAppService
    <
        ProductDto,
        Guid,
        PagedResultRequestDto
    >
    {
        Task<PagedResult<ProductInListDto>> GetListFilterAsync(ProductListFilterDto input);
        Task<List<ProductInListDto>> GetListAllAsync();
        Task<string> GetThumbnailImageAsync(string fileName);
        Task<List<ProductAttributeValueDto>> getListProductAttributeAllAsync(Guid productId);
        Task<PagedResult<ProductAttributeValueDto>> getListProductAttributesAsync(ProductAttributeListFilterDto input);
        Task<List<ProductInListDto>> GetListTopSellerAllAsync(int numberOfRecord);

        Task<List<ProductInListDto>> GetListTopSellerAsync(int numberOfRecords);
        Task<ProductDto> GetBySlugAsync(string slug);
    }
}
