namespace Talabat.Application.Common.Errors;
public record PaymentErrors
{
	public static readonly Error OrderNotFound =
		new("Payment.OrderNotFound", "No order was found with the given ID", StatusCodes.Status404NotFound);

	public static readonly Error PaymentFailed =
		new("Payment.Failed", "Payment processing failed", StatusCodes.Status402PaymentRequired);

	public static readonly Error OrderNotPending =
		new("Payment.OrderNotPending", "Order is not pending and cannot be paid", StatusCodes.Status409Conflict);

	public static readonly Error OrderCancelled =
		new("Payment.OrderCancelled", "Order is cancelled.", StatusCodes.Status409Conflict);

	public static readonly Error AlreadyPaid =
		new("Payment.AlreadyPaid", "Order is already paid", StatusCodes.Status409Conflict);
}