
namespace Products.APIs.Helper
{
    using Products.APIs.Models;
    using Products.Domain;
    using System.Collections.Generic;

    public class CategoryResponseFromCategories
    {
        public static CategoryResponse CategoryResponseFromCategory(Category category, List<ProductResponse> productsResponse)
        {
            return new CategoryResponse
            {
                CategoryId = category.CategoryId,
                Description = category.Description,
                Products = productsResponse
            };
        }
    }
}