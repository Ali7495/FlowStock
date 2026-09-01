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
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = Policies.ProductCategoryCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateProductCategory(ProductCategoryCommand productCategoryCommand, CancellationToken cancellationToken)
        {
            Guid id = await _mediator.Send(productCategoryCommand,cancellationToken);

            return CreatedAtAction("GetCategory",new {id}, new {id});
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken)
        {
            ProductCategoryDto productCategoryDto = await _mediator.Send(new GetProductCategoryByIdQuery(id),cancellationToken);

            return Ok(productCategoryDto);
        }

        [HttpGet(Name = "GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            List<ProductCategoryDto> productCategories = await _mediator.Send(new GetAllProductCategoryQuery(),cancellationToken);

            return Ok(productCategories);
        }

        [HttpPut("{id}", Name = "UpdateProductCategory")]
        public async Task<IActionResult> UpdateProductCategory(Guid id, ProductCategoryUpdateCommand productCategoryUpdateCommand, CancellationToken cancellationToken)
        {
            productCategoryUpdateCommand.id = id;

            await _mediator.Send(productCategoryUpdateCommand, cancellationToken);

            return Ok(NoContent());
        }

        [HttpDelete("{id}", Name ="DeleteProductCategory")]
        public async Task<IActionResult> DeleteProductCategory(Guid id, CancellationToken cancellationToken)
        {
            ProductCategoryDeleteCommand productCategoryDeleteCommand = new(id);

            await _mediator.Send(productCategoryDeleteCommand, cancellationToken);

            return Ok(NoContent());
        }
    }
}
