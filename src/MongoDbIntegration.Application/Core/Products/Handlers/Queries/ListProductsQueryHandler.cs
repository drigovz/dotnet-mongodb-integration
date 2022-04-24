using MediatR;
using MongoDbIntegration.Application.Core.Products.Queries;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Queries
{
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public ListProductsQueryHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(ListProductsQuery request, CancellationToken cancellationToken) =>
            new GenericResponse
            {
                Result = _productRepository.AsQueryable().ToList(),
            };
    }
}
