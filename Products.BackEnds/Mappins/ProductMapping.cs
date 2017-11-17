using Products.BackEnds.Models;
using Products.Domain;

namespace Products.BackEnds.Mappins
{
    public class ProductMapping
    {
        public static Product ProductViewToProduct(ProductView view)
        {
            return new Product
            {
                Category = view.Category,
                CategoryId = view.CategoryId,
                Description = view.Description,
                Image = view.Image,
                IsActive = view.IsActive,
                LastPurchase = view.LastPurchase,
                Price = view.Price,
                ProductId = view.ProductId,
                Remarks = view.Remarks,
                Stock = view.Stock
            };
        }
        public static ProductView ProductToProductView (Product product)
        {
            return new ProductView
            {
                Category = product.Category,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Image = product.Image,
                IsActive = product.IsActive,
                LastPurchase = product.LastPurchase,
                Price = product.Price,
                ProductId = product.ProductId,
                Remarks = product.Remarks,
                Stock = product.Stock,
            };
        }
    }
}