using BMHEcommerce.Public.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Public.Catalog.Products
{
    public class ProductListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
