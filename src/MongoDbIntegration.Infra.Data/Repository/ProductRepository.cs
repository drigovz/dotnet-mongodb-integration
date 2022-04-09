using MongoDbIntegration.Domain.Entities;
using MongoDbIntegration.Domain.Interfaces.MongoConfig;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Infra.Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IMongoDbSettings settings) 
            : base(settings)
        { }
    }
}
