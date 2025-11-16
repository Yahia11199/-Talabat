namespace Talabat.Application.Contracts;
public class UpdateOrderStatusRequestValidator : AbstractValidator<UpdateOrderStatusRequest>
{
	public UpdateOrderStatusRequestValidator()
	{
		RuleFor(x => x.Status)
			.IsInEnum()
			.WithMessage($"Status must be one of: {string.Join(", ", Enum.GetNames(typeof(OrderStatus)))}");
	}
}