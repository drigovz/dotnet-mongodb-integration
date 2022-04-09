using MediatR;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Entities;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly NotificationContext _notification;

        public CreateProductCommandHandler(IProductRepository productRepository, NotificationContext notification)
        {
            _productRepository = productRepository;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Title, request.Description, request.Price, request.Active);
            if (!product.Valid)
            {
                _notification.AddNotifications(product.ValidationResult);

                return new GenericResponse { Notifications = _notification.Notifications, };
            }

            await _productRepository.InsertOneAsync(product);
            //if (result == null)
            //{
            //    _notification.AddNotification("Error", "Error when try to add new Product");

            //    return new GenericResponse { Notifications = _notification.Notifications, };
            //}

            return new GenericResponse { Result = "ok", };
        }
    }
}
