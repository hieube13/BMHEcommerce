﻿using BMHEcommerce.Public.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public.Catalog.ProductAttributes
{
    public interface IProductAttributesAppService : IReadOnlyAppService
    <
        ProductAttributeDto,
        Guid,
        PagedResultRequestDto
    >
    {
        Task<PagedResult<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductAttributeInListDto>> GetListAllAsync();
    }
}
