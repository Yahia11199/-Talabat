namespace Talabat.Application.Contracts;

public record PayOrderResponse(
	int OrderId,
	decimal PaidAmount,
	string Currency,
	string TransactionId,
	bool IsSuccess,
	DateTime PaidAt
);
