using MediatR;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Core.Products.Queries;
using MongoDbIntegration.Application.Notifications;
using MongoDbIntegration.Domain.Interfaces.Repository;

namespace MongoDbIntegration.Application.Core.Products.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, GenericResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;
        private readonly NotificationContext _notification;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMediator mediator, NotificationContext notification)
        {
            _productRepository = productRepository;
            _mediator = mediator;
            _notification = notification;
        }

        public async Task<GenericResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _mediator.Send(new GetProductQuery
                {
                    Id = request.Id,
                });

                var updateProduct = product?.Result?.Update(
                        request.Title,
                        request.Description,
                        request.Price,
                        request.Active
                    );

                var result = _productRepository.ReplaceOneAsync(updateProduct);
                if (result?.Exception != null)
                {
                    _notification.AddNotification("Error", result?.Exception?.Message);

                    return new GenericResponse { Notifications = _notification.Notifications, };
                }

                return new GenericResponse
                {
                    Result = updateProduct,
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
