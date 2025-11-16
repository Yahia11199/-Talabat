namespace Talabat.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
	private readonly IRoleService _roleService;
	public RolesController(IRoleService roleService)
	{
		_roleService = roleService;
	}

	[HttpGet("")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> GetAll([FromQuery] bool includeDisabled, CancellationToken cancellation)
	{
		var roles = await _roleService.GetAllAsync(includeDisabled, cancellation);

		return Ok(roles);
	}

	[HttpGet("{id}")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> Get([FromRoute] string id)
	{
		var result = await _roleService.GetAsync(id);

		return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
	}


	[HttpPost("")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> Add([FromBody] RoleRequest request)
	{
		var result = await _roleService.AddAsync(request);

		return result.IsSuccess ? CreatedAtAction(nameof(Get), new { result.Value.Id }, result.Value) : result.ToProblem();
	}

	[HttpPut("{id}")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> Update([FromRoute] string id, [FromBody] RoleRequest request)
	{
		var result = await _roleService.UpdateAsync(id, request);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}

	[HttpPut("{id}/toggle-status")]
	[Authorize(Roles = DefaultRoles.Admin.Name)]
	public async Task<IActionResult> ToggleStatus([FromRoute] string id)
	{
		var result = await _roleService.ToggleStatusAsync(id);

		return result.IsSuccess ? NoContent() : result.ToProblem();
	}
}
