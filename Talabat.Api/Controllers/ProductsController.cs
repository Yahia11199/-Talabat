namespace Talabat.Api.Controllers;

[Route("api/stores/{storeId}/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly IProductService _productService;
	public ProductsController(IProductService productService)
	{
		_productService = productService;
	}

	[HttpGet("")]
	[EnableRateLimiting(RateLimiters.Concurrency)]
	[Authorize(Roles = $"{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> GetAll([FromRoute] int storeId, CancellationToken cancellationToken)
	{
		var result = await _productService.GetAllAsync(storeId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{id}")]
	[EnableRateLimiting(RateLimiters.Concurrency)]
	[Authorize(Roles = $"{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> Get([FromRoute] int storeId, [FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _productService.GetAsync(storeId, id, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> Add([FromRoute] int storeId, [FromBody] ProductRequest request, CancellationToken cancellationToken)
	{
		string ownerId = User.GetUserId()!;

		var result = await _productService.AddAsync(ownerId, storeId, request, cancellationToken);

		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { storeId, id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> Update([FromRoute] int storeId, [FromRoute] int id, [FromBody] ProductRequest request, CancellationToken cancellationToken)
	{
		string ownerId = User.GetUserId()!;

		var result = await _productService.UpdateAsync(ownerId, storeId, id, request, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggle-status")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> ToggleStatus([FromRoute] int storeId, [FromRoute] int id, CancellationToken cancellationToken)
	{
		string ownerId = User.GetUserId()!;

		var result = await _productService.ToggleStatusAsync(ownerId, storeId, id, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> Delete([FromRoute] int storeId, [FromRoute] int id, CancellationToken cancellationToken)
	{
		string ownerId = User.GetUserId()!;

		var result = await _productService.DeleteAsync(ownerId, storeId, id, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
