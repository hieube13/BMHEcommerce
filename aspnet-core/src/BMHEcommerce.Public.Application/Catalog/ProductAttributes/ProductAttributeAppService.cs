using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.ProductAttributes;
using BMHEcommerce.ProductCategories;
using BMHEcommerce.Public.Catalog.ProductAttributes;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;

namespace BMHEcommerce.Public.System.Catalog.ProductAttributes
{
    public class ProductAttributeAppService : ReadOnlyAppService
    <
        ProductAttribute,
        ProductAttributeDto,
        Guid,
        PagedResultRequestDto
    >, IProductAttributesAppService
    {

        public ProductAttributeAppService(Volo.Abp.Domain.Repositories.IRepository<ProductAttribute, Guid> repository) : base(repository)
        {
            
        }


        public async Task<List<ProductAttributeInListDto>> GetListAllAsync()
        {
            var query = await Repository.GetQueryableAsync();
            query = query.Where(x => x.IsActive == true);
            var data = await AsyncExecuter.ToListAsync(query);

            return ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data);

        }

        public async Task<PagedResult<ProductAttributeInListDto>> GetListFilterAsync(BaseListFilterDto input)
        {
            var query = await Repository.GetQueryableAsync();
            query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Keyword), x => x.Label.Contains(input.Keyword));

            var totalCount = await AsyncExecuter.LongCountAsync(query);
            var data = await AsyncExecuter
               .ToListAsync(
                  query.Skip((input.CurrentPage - 1) * input.PageSize)
               .Take(input.PageSize));

            return new PagedResult<ProductAttributeInListDto>(
                ObjectMapper.Map<List<ProductAttribute>, List<ProductAttributeInListDto>>(data),
                totalCount,
                input.CurrentPage,
                input.PageSize
            );
        }
    }
}
