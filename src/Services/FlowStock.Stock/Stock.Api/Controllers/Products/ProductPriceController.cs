using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stock.Application;
namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductPriceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductPriceController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> AddProductPrice(ProductPriceCommand productPriceCommand, CancellationToken cancellationToken)
        {
            ProductPriceDto product = await _mediator.Send(productPriceCommand,cancellationToken);

            return Ok(product);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetActivePriceByProduct(Guid productId, CancellationToken cancellationToken)
        {
            GetActivePriceByProductIdQuery query = new (productId);

            ProductPriceDto productPriceDto = await _mediator.Send(query,cancellationToken);

            return Ok(productPriceDto);
        }

        [HttpGet("prices/{productId}")]
        public async Task<IActionResult> GetPricesOfProduct(Guid productId, CancellationToken cancellationToken)
        {
            GetAllPricesByProductQuery query = new(productId);

            List<ProductPriceAllInfoDto> allInfoDtos = await _mediator.Send(query, cancellationToken);

            return Ok(allInfoDtos);
        }

        [HttpPut("price/update")]
        public async Task<IActionResult> UpdateProductPrice(UpdateProductPriceCommand command, CancellationToken cancellationToken)
        {
            ProductPriceDto productPriceDto = await _mediator.Send(command, cancellationToken);

            return Ok(productPriceDto);
        }
    }
}
