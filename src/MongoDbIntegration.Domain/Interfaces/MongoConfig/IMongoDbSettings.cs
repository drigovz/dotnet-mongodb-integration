namespace MongoDbIntegration.Domain.Interfaces.MongoConfig
{
    public interface IMongoDbSettings
    {
        string DatabaseName { get; set; }
        string ConnectionString { get; set; }
    }
}
