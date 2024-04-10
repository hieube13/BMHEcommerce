using BMHEcommerce.Admin.Catalog.ProductAttributes;
using BMHEcommerce.Admin.Catalog.ProductCategories;
using BMHEcommerce.Admin.Permissions;
using BMHEcommerce.ProductAttributes;
using BMHEcommerce.ProductCategories;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace BMHEcommerce.Admin.System.Catalog.ProductAttributes
{
    [Authorize(BMHEcommercePermissions.Attribute.Default, Policy = "AdminOnly")]
    public class ProductAttributeAppService : CrudAppService
    <
        ProductAttribute,
        ProductAttributeDto,
        Guid,
        PagedResultRequestDto,
        CreateUpdateProductAttributeDto,
        CreateUpdateProductAttributeDto
    >, IProductAttributesAppService
    {

        public ProductAttributeAppService(Volo.Abp.Domain.Repositories.IRepository<ProductAttribute, Guid> repository) : base(repository)
        {
            GetPolicyName = BMHEcommercePermissions.Attribute.Default;
            GetListPolicyName = BMHEcommercePermissions.Attribute.Default;
            CreatePolicyName = BMHEcommercePermissions.Attribute.Create;
            UpdatePolicyName = BMHEcommercePermissions.Attribute.Update;
            DeletePolicyName = BMHEcommercePermissions.Attribute.Delete;
        }

        [Authorize(BMHEcommercePermissions.Attribute.Delete)]
        public async Task DeleteMultipleAsync(IEnumerable<Guid> ids)
        {
            await Repository.DeleteManyAsync(ids);
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        [Authorize(BMHEcommercePermissions.Attribute.Default)]
        public async Task<List<ProductAttributeInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data);

        }

        [Authorize(BMHEcommercePermissions.Attribute.Default)]
        public async Task<PagedResultDto<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();

            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Label.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);

            var data = await AsyncExecuter.ToListAsync(query.Skip(input.SkipCount).Take(input.MaxResultCount));

            return new PagedResultDto<ProductAttributeInListDto>(totalCount, ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data));
        }
    }
}
