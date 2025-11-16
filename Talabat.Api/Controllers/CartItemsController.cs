namespace Talabat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemsController : ControllerBase
{
	private readonly ICartItemService _cartItemService;
	public CartItemsController(ICartItemService cartItemService)
	{
		_cartItemService = cartItemService;
	}

	[HttpGet("")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> GetAllItems(CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartItemService.GetAllItemsAsync(userId!, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpGet("{itemId}")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> GetItem([FromRoute] int itemId, CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartItemService.GetItemByIdAsync(userId!, itemId, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPut("{itemId}")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> UpdateQuantity([FromRoute] int itemId, [FromBody] UpdateQuantityRequest request, CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartItemService.UpdateQuantityAsync(userId!, itemId, request.Quantity, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}