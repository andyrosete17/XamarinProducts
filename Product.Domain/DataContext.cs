
namespace Products.Domain
{
    using System.Data.Entity;

    public class DataContext :  DbContext
    {
        public DataContext() :base("ProductsConnection")
        {

        }

    }
}
