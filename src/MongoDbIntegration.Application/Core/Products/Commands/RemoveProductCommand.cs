using MediatR;

namespace MongoDbIntegration.Application.Core.Products.Commands
{
    public class RemoveProductCommand : IRequest<GenericResponse>
    {
        public string Title { get; set; }
    }
}
