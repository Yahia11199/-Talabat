namespace Talabat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartsController : ControllerBase
{
	private readonly ICartService _cartService;
	public CartsController(ICartService cartService)
	{
		_cartService = cartService;
	}

	[HttpGet("")]
	[Authorize(Roles = $"{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartService.GetUserCartAsync(userId!, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("items")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> AddItem([FromBody] AddToCartRequest request, CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartService.AddToCartAsync(userId!, request, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpDelete("items/{itemId}")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> RemoveItem(int itemId, CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartService.RemoveItemAsync(userId!, itemId, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpDelete("clear")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _cartService.ClearCartAsync(userId!, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}