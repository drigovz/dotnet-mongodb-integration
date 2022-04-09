using MediatR;

namespace MongoDbIntegration.Application.Core.Products.Commands
{
    public class ProductCommand : IRequest<GenericResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
    }
}
