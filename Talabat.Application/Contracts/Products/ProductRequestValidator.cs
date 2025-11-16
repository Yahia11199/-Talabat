namespace Talabat.Application.Contracts;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
	public ProductRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(3, 100);

		RuleFor(x => x.Description)
		.NotEmpty()
			.Length(3, 1000);

		RuleFor(x => x.Department)
		.NotEmpty()
			.Length(3, 100);

		RuleFor(x => x.Price)
		.NotEmpty()
			.GreaterThanOrEqualTo(1);

		RuleFor(x => x.Quantity)
		.NotEmpty()
			.GreaterThanOrEqualTo(1);
	}
}