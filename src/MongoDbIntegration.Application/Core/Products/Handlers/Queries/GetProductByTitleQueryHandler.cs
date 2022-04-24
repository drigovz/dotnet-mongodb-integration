using MediatR;
using MongoDbIntegration.Application.Core.Products.Queries;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Queries
{
    public class GetProductByTitleQueryHandler : IRequestHandler<GetProductByTitleQuery, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public GetProductByTitleQueryHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(GetProductByTitleQuery request, CancellationToken cancellationToken)
        {
            var product = _productRepository.FindOneAsync(x => x.Title.ToLower() == request.Title.ToLower())?.Result;
            if (product == null)
            {
                _notification.AddNotification("Error", $"Product with title: '{request.Title}' not found!");

                return new GenericResponse { Notifications = _notification.Notifications, };
            }

            return new GenericResponse { Result = product, };
        }
    }
}
