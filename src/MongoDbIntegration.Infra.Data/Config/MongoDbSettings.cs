using MongoDbIntegration.Domain.Interfaces.MongoConfig;

namespace MongoDbIntegration.Infra.Data.Config
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
