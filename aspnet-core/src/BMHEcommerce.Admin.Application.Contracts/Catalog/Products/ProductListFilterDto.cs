using BMHEcommerce.Admin.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Admin.Catalog.Products
{
    public class ProductListFilterDto : BaseListFilterDto
    {
        public Guid? CategoryId { get; set; }
    }
}
