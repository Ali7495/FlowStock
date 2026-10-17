using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Application;
using Stock.Domain;
using Stock.Infrastructure;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductPriceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = Policies.ProductPriceCreate)]
        [HttpPost]
        public async Task<IActionResult> AddProductPrice(ProductPriceCommand productPriceCommand, CancellationToken cancellationToken)
        {
            Product product = await _mediator.Send(productPriceCommand,cancellationToken);

            return Ok(product);
        }
    }
}
