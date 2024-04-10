using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.Public.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace BMHEcommerce.Public.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IProductCategoriesAppService _productCategoriesAppService;

        private readonly IDistributedCache<HeaderCacheItem> _distributedCache;

        public HeaderViewComponent(IProductCategoriesAppService productCategoriesAppService, IDistributedCache<HeaderCacheItem> distributedCache) 
        { 
            _productCategoriesAppService = productCategoriesAppService;
            _distributedCache = distributedCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cacheItem = await _distributedCache.GetOrAddAsync(BMHEcommercePublicConstants.CacheKeys.HeaderData, async() =>
            {
                var model = await _productCategoriesAppService.GetListAllAsync();
                return new HeaderCacheItem()
                {
                    Categories = model
                };
            },
            () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
            });

            return View(cacheItem.Categories);
        }
    }
}
