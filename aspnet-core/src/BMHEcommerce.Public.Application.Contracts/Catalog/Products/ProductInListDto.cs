﻿using BMHEcommerce.Products;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace BMHEcommerce.Public.Catalog.Products
{
    public class ProductInListDto : EntityDto<Guid>
    {
        public Guid ManufacturerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Slug { get; set; }
        public ProductType ProductType { get; set; }
        public string SKU { get; set; }
        public int SortOrder { get; set; }
        public bool Visiblity { get; set; }
        public bool IsActive { get; set; }
        public Guid CategoryId { get; set; }
        public string ThumbnailPicture { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public double SellPrice { get; set; }
    }
}
