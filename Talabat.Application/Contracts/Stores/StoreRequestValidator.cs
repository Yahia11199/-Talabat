namespace Talabat.Application.Contracts;

public class StoreRequestValidator : AbstractValidator<StoreRequest>
{
	public StoreRequestValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty()
			.Length(3, 100);

		RuleFor(x => x.Description)
		.NotEmpty()
			.Length(3, 1000);

		RuleFor(x => x.Address)
		.NotEmpty()
			.Length(3, 250);

		RuleFor(x => x.Category)
		.NotEmpty()
			.Length(3, 100);

		RuleFor(x => x.PhoneNumber)
			.NotEmpty()
			.Matches(RegexPatterns.EgyptianPhone)
			.WithMessage("Invalid Egyptian phone number format.");
	}
}
