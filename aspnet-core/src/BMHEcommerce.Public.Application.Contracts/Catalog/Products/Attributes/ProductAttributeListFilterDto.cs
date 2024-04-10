using BMHEcommerce.Public.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Public.Catalog.Products.Attributes
{
    public class ProductAttributeListFilterDto : BaseListFilterDto
    {
        public Guid ProductID { get; set; }
    }
}
