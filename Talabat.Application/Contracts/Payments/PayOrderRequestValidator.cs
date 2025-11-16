namespace Talabat.Application.Contracts;

public class PayOrderRequestValidator : AbstractValidator<PayOrderRequest>
{
	public PayOrderRequestValidator()
	{
		RuleFor(x => x.PaymentToken)
			.NotEmpty();
	}
}
