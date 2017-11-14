
namespace Products.Domain
{
    using System.Data.Entity;

    public class DataContext :  DbContext
    {
        public DataContext() :base("ProductsConnection")
        {

        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
