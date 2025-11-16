namespace Talabat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableRateLimiting(RateLimiters.Concurrency)]
public class OrdersController : ControllerBase
{
	private readonly IOrderService _orderService;
	public OrdersController(IOrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpGet("")]
	[Authorize(Roles = $"{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _orderService.GetUserOrdersAsync(userId!, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> CreateOrder(CancellationToken cancellationToken)
	{
		var userId = User.GetUserId();

		var result = await _orderService.CreateOrderAsync(userId!, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPut("{orderId}/status")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> UpdateOrderStatus([FromRoute] int orderId, [FromBody] UpdateOrderStatusRequest request, CancellationToken cancellationToken)
	{
		var result = await _orderService.UpdateOrderStatusAsync(orderId, request.Status, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}