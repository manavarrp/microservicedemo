using Catalog.Api.Context;
using Catalog.Api.Interfaces.Repositories;
using Catalog.Api.Models;
using MongoRepo.Repository;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : CommonRepository<Product>, IProductRepository
    {
        public ProductRepository() : base(new CatalogDbContext())
        {
        }
    }
}
