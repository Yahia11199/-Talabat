namespace Talabat.Application.Contracts;
public class AddToCartRequestValidator : AbstractValidator<AddToCartRequest>
{
	public AddToCartRequestValidator()
	{
		RuleFor(x => x.ProductId)
		.NotEmpty()
			.GreaterThanOrEqualTo(1);

		RuleFor(x => x.Quantity)
		.NotEmpty()
			.GreaterThanOrEqualTo(1);
	}
}