using MongoDbIntegration.Application.Core.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MongoDbIntegration.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request)
            => Ok(await _mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] RemoveProductCommand request)
            => Ok(await _mediator.Send(request));
    }
}
