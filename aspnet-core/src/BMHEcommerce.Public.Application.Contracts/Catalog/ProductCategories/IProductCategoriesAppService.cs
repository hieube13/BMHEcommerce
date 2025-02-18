﻿using BMHEcommerce.Public;
using BMHEcommerce.Public.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BMHEcommerce.Public.Catalog.ProductCategories
{
    public interface IProductCategoriesAppService : IReadOnlyAppService
    <
        ProductCategoryDto,
        Guid,
        PagedResultRequestDto
    >
    {
        Task<PagedResult<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input);
        Task<List<ProductCategoryInListDto>> GetListAllAsync();
        Task<ProductCategoryDto> GetByCodeAsync(string code);
        Task<ProductCategoryDto> GetBySlugAsync(string slug);
    }
}
