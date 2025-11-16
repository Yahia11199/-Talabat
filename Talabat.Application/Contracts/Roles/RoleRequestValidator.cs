namespace Talabat.Application.Contracts;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
	public RoleRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(3, 200);
	}
}