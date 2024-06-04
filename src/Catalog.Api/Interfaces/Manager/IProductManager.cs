using Catalog.Api.Models;
using MongoRepo.Interfaces.Manager;

namespace Catalog.Api.Interfaces.Manager
{
    public interface IProductManager : ICommonManager<Product>
    {
       public List<Product> GetByCategory(string category);
    }
}
