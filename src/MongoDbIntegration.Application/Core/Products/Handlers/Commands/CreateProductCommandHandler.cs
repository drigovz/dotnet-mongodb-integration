using MediatR;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Core.Products.Queries;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Entities;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notification;

        public CreateProductCommandHandler(IProductRepository productRepository, IMediator mediator, NotificationContext notification)
        {
            _productRepository = productRepository;
            _mediator = mediator;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product(request.Title, request.Description, request.Price, request.Active);
                if (!product.Valid)
                {
                    _notification.AddNotifications(product.ValidationResult);

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                var result = await _mediator.Send(new GetProductByTitleQuery 
                {
                    Title = request.Title,
                });

                if (result?.Result == null)
                    await _productRepository.InsertOneAsync(result?.Result);
                else
                {
                    _notification.AddNotification("Error", $"Product with title: '{product.Title}' already exists!");

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                return new GenericResponse { Result = "ok", };
            }
            catch (Exception ex)
            {
                _notification.AddNotification("Error", ex.Message);

                return new GenericResponse { Notifications = _notification.Notifications, };
            }
        }
    }
}
