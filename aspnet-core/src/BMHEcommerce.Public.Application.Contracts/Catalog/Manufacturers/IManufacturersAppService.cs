using BMHEcommerce.Public.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public.Catalog.Manufacturers
{
    public interface IManufacturersAppService : IReadOnlyAppService
    <
        ManufacturerDto,
        Guid,
        PagedResultRequestDto
    >
    {
        Task<PagedResult<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ManufacturerInListDto>> GetListAllAsync();
    }
}
