using Catalog.Api.Interfaces.Manager;
using Catalog.Api.Models;
using Catalog.Api.Repositories;
using MongoRepo.Manager;

namespace Catalog.Api.Manager
{
    public class ProductManager : CommonManager<Product>, IProductManager
    {
        public ProductManager() : base(new ProductRepository())
        {
        }

        public List<Product> GetByCategory(string category)
        {
            return GetAll(c => c.Category == category).ToList();
        }
    }
}
