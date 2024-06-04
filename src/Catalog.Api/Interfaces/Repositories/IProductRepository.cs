using Catalog.Api.Models;
using MongoRepo.Interfaces.Repository;

namespace Catalog.Api.Interfaces.Repositories
{
    public interface IProductRepository : ICommonRepository<Product>
    {
    }
}
