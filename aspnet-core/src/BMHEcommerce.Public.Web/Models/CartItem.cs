using BMHEcommerce.Public.Catalog.Products;

namespace BMHEcommerce.Public.Web.Models
{
    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
