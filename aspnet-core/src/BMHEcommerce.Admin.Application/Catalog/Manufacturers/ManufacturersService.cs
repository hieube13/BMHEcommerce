﻿using BMHEcommerce.Admin.Catalog.Manufacturers;
using BMHEcommerce.Admin.Catalog.ProductCategories;
using BMHEcommerce.Admin.Permissions;
using BMHEcommerce.Manufacturers;
using BMHEcommerce.Products;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BMHEcommerce.Admin.System.Catalog.Manufacturers
{
    [Authorize(BMHEcommercePermissions.Manufacturer.Default, Policy = "AdminOnly")]
    public class ManufacturersAppService : CrudAppService
    <
        Manufacturer,
        ManufacturerDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateManufacturerDto,
        CreateUpdateManufacturerDto
    >, IManufacturersAppService
    {
        public ManufacturersAppService(IRepository<Manufacturer, Guid> repository) : base(repository)
        {
            GetPolicyName = BMHEcommercePermissions.Manufacturer.Default;
            GetListPolicyName = BMHEcommercePermissions.Manufacturer.Default;
            CreatePolicyName = BMHEcommercePermissions.Manufacturer.Create;
            UpdatePolicyName = BMHEcommercePermissions.Manufacturer.Update;
            DeletePolicyName = BMHEcommercePermissions.Manufacturer.Delete;
        }

        [Authorize(BMHEcommercePermissions.Manufacturer.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(BMHEcommercePermissions.Manufacturer.Default)]
        public async Task<List<ManufacturerInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();

            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data);
        }

        [Authorize(BMHEcommercePermissions.Manufacturer.Default)]
        public async Task<PagedResultDto<ManufacturerInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ManufacturerInListDto>(totalCount, ObjectMapper.Map<List<Manufacturer>, List<ManufacturerInListDto>>(data));
        }
    }
}
