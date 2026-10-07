using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Application;
using Stock.Infrastructure;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = Policies.ProductCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCommand productCommand, CancellationToken cancellationToken)
        {
            Guid id = await _mediator.Send(productCommand, cancellationToken);

            return CreatedAtAction("GetProductById", new{id}, id);
        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<IActionResult> GetProductById(Guid id, CancellationToken cancellationToken)
        {
            ProductDto productDto = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);

            return Ok(productDto);
        }
    }
}
