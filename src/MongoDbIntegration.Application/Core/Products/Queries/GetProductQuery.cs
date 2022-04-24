using MediatR;

namespace MongoDbIntegration.Application.Core.Products.Queries
{
    public class GetProductQuery : IRequest<GenericResponse>
    {
        public string Id { get; set; }
    }
}
