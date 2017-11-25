
namespace Products.APIs.Helper
{
    using Products.APIs.Models;
    using Products.Domain;
    public class ProductResponseFromProduct
    {
        public static ProductResponse ProductResponseFromProducts(Product product)
        {
            return new ProductResponse
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                Image = product.Image,
                Stock = product.Stock,
                LastPurchase = product.LastPurchase,
                Remarks = product.Remarks
            };
        }
    }
}