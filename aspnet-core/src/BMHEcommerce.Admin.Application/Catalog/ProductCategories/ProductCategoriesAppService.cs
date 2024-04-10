using BMHEcommerce.Admin.Catalog.ProductCategories;
using BMHEcommerce.Admin.Permissions;
using BMHEcommerce.ProductCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.OpenIddict;

namespace BMHEcommerce.Admin.System.Catalog.ProductCategories
{
    [Authorize(BMHEcommercePermissions.ProductCategory.Default, Policy = "AdminOnly")]
    public class ProductCategoriesAppService : CrudAppService
        <
            ProductCategory,
            ProductCategoryDto,
            Guid,
            PagedResultRequestDto,
            CreateUpdateProductCategoryDto,
            CreateUpdateProductCategoryDto
        >, IProductCategoriesAppService
    {
        //private readonly IRepository<ProductCategory, Guid> _repository;
        public ProductCategoriesAppService(IRepository<ProductCategory, Guid> repository) : base(repository)
        {
            GetPolicyName = BMHEcommercePermissions.ProductCategory.Default;
            GetListPolicyName = BMHEcommercePermissions.ProductCategory.Default;
            CreatePolicyName = BMHEcommercePermissions.ProductCategory.Create;
            UpdatePolicyName = BMHEcommercePermissions.ProductCategory.Update;
            DeletePolicyName = BMHEcommercePermissions.ProductCategory.Delete;
        }

        [Authorize(BMHEcommercePermissions.ProductCategory.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(BMHEcommercePermissions.ProductCategory.Default)]
        public async Task<List<ProductCategoryInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);

            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data);
        }

        [Authorize(BMHEcommercePermissions.ProductCategory.Default)]
        public async Task<PagedResultDto<ProductCategoryInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Name.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductCategoryInListDto>(totalCount, ObjectMapper.Map<List<ProductCategory>, List<ProductCategoryInListDto>>(data));


        }
    }
}
