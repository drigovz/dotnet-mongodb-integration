using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDbIntegration.Application.Core.Products.Commands;
using MongoDbIntegration.Application.Core.Products.Queries;

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
        public async Task<IActionResult> Create([FromBody] CreateProductCommand request) =>
            Ok(await _mediator.Send(request));

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] RemoveProductCommand request) =>
            Ok(await _mediator.Send(request));

        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request) =>
            Ok(await _mediator.Send(request));

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new ListProductsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id) =>
            Ok(await _mediator.Send(new GetProductQuery { Id = id }));

        [HttpGet("title/{title}")]
        public async Task<IActionResult> GetByTitle([FromRoute] string title) =>
            Ok(await _mediator.Send(new GetProductByTitleQuery { Title = title }));
    }
}
