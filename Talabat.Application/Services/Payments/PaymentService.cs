namespace Talabat.Application.Services;

public class PaymentService : IPaymentService
{
	private readonly IUnitOfWork _unitOfWork;
	public PaymentService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<PayOrderResponse>> PayOrderAsync(int orderId, PayOrderRequest request, CancellationToken cancellationToken)
	{
		var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(orderId, cancellationToken);

		if (order is null)
			return Result.Failure<PayOrderResponse>(PaymentErrors.OrderNotFound);

		if (order.Status == OrderStatus.Cancelled)
			return Result.Failure<PayOrderResponse>(PaymentErrors.OrderCancelled);

		if (order.IsPaid)
			return Result.Failure<PayOrderResponse>(PaymentErrors.AlreadyPaid);

		if (order.Status != OrderStatus.Pending)
			return Result.Failure<PayOrderResponse>(PaymentErrors.OrderNotPending);

		var amountDecimal = order.Items.Sum(i => i.UnitPrice * i.Quantity);
		var amountInCents = (long)Math.Round(amountDecimal * 100m);


		var options = new PaymentIntentCreateOptions
		{
			Amount = amountInCents,
			Currency = "usd",
			PaymentMethod = request.PaymentToken,
			Confirm = true,
			Description = $"Payment for Order {order.Id}"
		};

		var paymentIntentService = new PaymentIntentService();
		PaymentIntent paymentIntent;

		try
		{
			paymentIntent = await paymentIntentService.CreateAsync(options, cancellationToken: cancellationToken);
		}
		catch (StripeException)
		{
			return Result.Failure<PayOrderResponse>(PaymentErrors.PaymentFailed);
		}

		var payment = new Payment
		{
			OrderId = order.Id,
			Amount = amountDecimal,
			Currency = "usd",
			PaymentMethod = "card",
			TransactionId = paymentIntent.Id,
			IsSuccess = paymentIntent.Status == "succeeded",
			PaidAt = DateTime.UtcNow
		};

		await _unitOfWork.Payments.AddAsync(payment, cancellationToken);

		if (payment.IsSuccess)
		{
			order.IsPaid = true;
			order.PaidAt = payment.PaidAt;
			order.Status = OrderStatus.Confirmed;
		}

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		var response = new PayOrderResponse(
			order.Id,
			payment.Amount,
			payment.Currency,
			payment.TransactionId,
			payment.IsSuccess,
			payment.PaidAt
		);

		return Result.Success(response);
	}
}
