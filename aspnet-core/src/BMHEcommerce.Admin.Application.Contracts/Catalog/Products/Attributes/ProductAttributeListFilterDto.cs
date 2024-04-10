using BMHEcommerce.Admin.Catalog.ProductCategories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMHEcommerce.Admin.Catalog.Products.Attributes
{
    public class ProductAttributeListFilterDto : BaseListFilterDto
    {
        public Guid ProductID { get; set; }
    }
}
