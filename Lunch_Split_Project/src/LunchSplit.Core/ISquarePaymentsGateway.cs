using Square;
using Square.Payments;

public interface ISquarePaymentsGateway
{
  Task<CreatePaymentResponse> CreatePaymentAsync(
    CreatePaymentRequest request);

  Task<GetPaymentResponse> GetPaymentAsync(
    string paymentId);

}