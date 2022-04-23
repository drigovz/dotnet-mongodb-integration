using MediatR;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public UpdateProductCommandHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _productRepository.FindByIdAsync(request.Id.ToString())?.Result;
                if (product == null)
                {
                    _notification.AddNotification("Error", $"Product with Id: '{request.Id}' not found!");

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                var result = _productRepository.ReplaceOneAsync(product);
                if (result?.Exception != null)
                {
                    _notification.AddNotification("Error", result?.Exception?.Message);

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                return new GenericResponse
                {
                    Result = "ok",
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
