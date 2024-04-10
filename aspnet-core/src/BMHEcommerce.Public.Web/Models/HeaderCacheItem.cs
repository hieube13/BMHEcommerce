using BMHEcommerce.Public.Catalog.ProductCategories;
using System.Collections.Generic;

namespace BMHEcommerce.Public.Web.Models
{
    public class HeaderCacheItem
    {
        public List<ProductCategoryInListDto> Categories { set; get; }
    }
}
