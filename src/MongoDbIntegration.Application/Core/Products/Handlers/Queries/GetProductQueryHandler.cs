using MediatR;
using MongoDbIntegration.Application.Core.Products.Queries;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Queries
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public GetProductQueryHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.FindByIdAsync(request.Id.ToString())?.Result;
            if (product == null)
            {
                _notification.AddNotification("Error", $"Product with Id: '{request.Id}' not found!");

                return new GenericResponse { Notifications = _notification.Notifications, };
            }

            return new GenericResponse
            {
                Result = product,
            };
        }
    }
}
