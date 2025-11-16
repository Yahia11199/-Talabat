namespace Talabat.Api.Controllers;

[ApiVersion(1, Deprecated = true)]
[ApiVersion(2)]
[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
	private readonly IStoreService _storeService;
	public StoresController(IStoreService storeService)
	{
		_storeService = storeService;
	}

	[HttpGet("")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		var response = await _storeService.GetAllAsync(cancellationToken);

		return Ok(response);
	}

	[MapToApiVersion(1)]
	[HttpGet("available")]
	[EnableRateLimiting(RateLimiters.UserLimiter)]
	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> GetAvailableV1([FromQuery] RequestFilters filters, CancellationToken cancellationToken)
	{
		var result = await _storeService.GetAvailableV1(filters, cancellationToken);

		return Ok(result.Value);
	}

	[MapToApiVersion(2)]
	[HttpGet("available")]
	[EnableRateLimiting(RateLimiters.UserLimiter)]
	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> GetAvailableV2([FromQuery] RequestFilters filters, CancellationToken cancellationToken)
	{
		var result = await _storeService.GetAvailableV2(filters, cancellationToken);

		return Ok(result.Value);
	}

	[HttpGet("{id}")]
	[EnableRateLimiting(RateLimiters.UserLimiter)]
	[Authorize(Roles = $"{DefaultRoles.Admin.Name},{DefaultRoles.Owner.Name},{DefaultRoles.Member.Name}")]
	public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _storeService.GetAsync(id, cancellationToken);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}

	[HttpPost("AddByOwner")]
	[Authorize(Roles = DefaultRoles.Owner.Name)]
	public async Task<IActionResult> AddByOwner([FromBody] StoreRequest request, CancellationToken cancellationToken)
	{
		var OwnerId = User.GetUserId()!;

		var result = await _storeService.AddByOwnerAsync(OwnerId, request, cancellationToken);

		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPost("AddByAdmin")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> AddByAdmin([FromBody] AddStoreRequestByAdmin request, CancellationToken cancellationToken)
	{
		var result = await _storeService.AddByAdminAsync(request, cancellationToken);

		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { id = result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = $"{DefaultRoles.Owner.Name},{DefaultRoles.Admin.Name}")]
	public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StoreRequest request, CancellationToken cancellationToken)
	{
		var OwnerId = User.GetUserId()!;

		var result = await _storeService.UpdateAsync(OwnerId, id, request, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggle-status")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> ToggleStatus([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _storeService.ToggleStatusAsync(id, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpDelete("{id}")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
	{
		var result = await _storeService.DeleteAsync(id, cancellationToken);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}