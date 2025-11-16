namespace Talabat.Api.Controllers;

[Route("api/orders/{orderId}/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
	private readonly IPaymentService _paymentService;
	public PaymentsController(IPaymentService paymentService)
	{
		_paymentService = paymentService;
	}

	[HttpPost("")]
	[Authorize(Roles = DefaultRoles.Member.Name)]
	public async Task<IActionResult> Pay([FromRoute] int orderId, [FromBody] PayOrderRequest request, CancellationToken cancellationToken)
	{
		var result = await _paymentService.PayOrderAsync(orderId, request, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}
}