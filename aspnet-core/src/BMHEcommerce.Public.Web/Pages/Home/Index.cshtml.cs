using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.Public.Catalog.Products;
using BMHEcommerce.Public.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Caching;

namespace BMHEcommerce.Public.Web.Pages;

public class IndexModel : PublicPageModel
{
    public List<ProductCategoryInListDto> Categories { get; set; }
    public List<ProductInListDto> TopSellerProducts { get; set; }

    private readonly IProductCategoriesAppService _productCategoriesAppService;

    private readonly IProductsAppService _productsAppService;

    private readonly IDistributedCache<HomeCacheItem> _distributedCache;

    public IndexModel(IProductCategoriesAppService productCategoriesAppService, IProductsAppService productsAppService, IDistributedCache<HomeCacheItem> distributedCache)
    {
        _productCategoriesAppService = productCategoriesAppService;
        _productsAppService = productsAppService;
        _distributedCache = distributedCache;
    }

    public async Task OnGetAsync()
    {
        var cacheItem = await _distributedCache.GetOrAddAsync(BMHEcommercePublicConstants.CacheKeys.HomeData, async () =>
        {
            var allCategories = await _productCategoriesAppService.GetListAllAsync();

            var rootCategories = allCategories.Where(x => x.ParentId == null).ToList();

            foreach (var category in rootCategories)
            {
                category.Children = rootCategories.Where(x => x.ParentId == category.Id).ToList();
            }

            var topSellerProducts = await _productsAppService.GetListTopSellerAllAsync(10);

            return new HomeCacheItem()
            { 
                Categories = rootCategories,
                TopSellerProducts = topSellerProducts
            };

        }, 
        () => new Microsoft.Extensions.Caching.Distributed.DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddHours(12)
        });


        Categories = cacheItem.Categories;
        TopSellerProducts = cacheItem.TopSellerProducts;
    }
}
