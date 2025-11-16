namespace Talabat.Application.Contracts;
public class UpdateQuantityRequestValidator : AbstractValidator<UpdateQuantityRequest>
{
	public UpdateQuantityRequestValidator()
	{
		RuleFor(x => x.Quantity)
		.NotEmpty()
			.GreaterThanOrEqualTo(1);
	}
}