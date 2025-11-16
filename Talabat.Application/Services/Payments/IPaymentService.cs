namespace Talabat.Application.Services;
public interface IPaymentService
{
	Task<Result<PayOrderResponse>> PayOrderAsync(int orderId, PayOrderRequest request, CancellationToken cancellationToken);
}