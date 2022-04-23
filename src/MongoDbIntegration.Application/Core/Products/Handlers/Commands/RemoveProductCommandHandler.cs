using MediatR;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Commands
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public RemoveProductCommandHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var title = request.Title.ToLower();

                var product = _productRepository.FindOneAsync(x => x.Title.ToLower() == title)?.Result;
                if (product == null)
                {
                    _notification.AddNotification("Error", $"Product with title: '{title}' not found!");

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                var result = _productRepository.DeleteOneAsync(x => x.Title.ToLower() == title);
                if (result?.Exception != null)
                {
                    _notification.AddNotification("Error", result?.Exception?.Message);

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                return new GenericResponse
                {
                    Result = "Product deleted!",
                };
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Error", ex.Message);

                return new GenericResponse { Notifications = _notification.Notifications, };
            }
        }
    }
}
