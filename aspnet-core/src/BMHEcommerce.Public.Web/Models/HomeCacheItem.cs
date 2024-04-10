using BMHEcommerce.Public.Catalog.ProductCategories;
using BMHEcommerce.Public.Catalog.Products;
using System.Collections.Generic;

namespace BMHEcommerce.Public.Web.Models
{
    public class HomeCacheItem
    {
        public List<ProductCategoryInListDto> Categories { set; get; }
        public List<ProductInListDto> TopSellerProducts { set; get; }
    }
}
