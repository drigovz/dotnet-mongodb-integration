using MongoDB.Bson;

namespace MongoDbIntegration.Application.Core.Products.Commands
{
    public class UpdateProductCommand : ProductCommand
    {
        public string Id { get; set; }

        public UpdateProductCommand(string id)
        {
            Id = id;
        }
    }
}
