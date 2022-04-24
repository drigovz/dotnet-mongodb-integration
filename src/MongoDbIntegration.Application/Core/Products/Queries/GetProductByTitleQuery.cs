using MediatR;

namespace MongoDbIntegration.Application.Core.Products.Queries
{
    public class GetProductByTitleQuery : IRequest<GenericResponse>
    {
        public string Title { get; set; }
    }
}
