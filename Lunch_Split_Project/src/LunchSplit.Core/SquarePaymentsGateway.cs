using Square;
using Square.Payments;

//Sends payment requests to square
public class SquarePaymentsGateway : ISquarePaymentsGateway
{
  //Create a new client that links the Square's UI with the backend
  private readonly SquareClient _client;

  //Constructor that gets and assigned the client object
  public SquarePaymentsGateway(SquareClient client)
  {
    _client = client;
  }

  //Create a new payment request for the client object
  public Task<CreatePaymentResponse> CreatePaymentAsync(
    CreatePaymentRequest request)
  {
    return _client.Payments.CreateAsync(request); 
  }

  public Task<GetPaymentResponse> GetPaymentAsync(string paymentId)
  {
      
      //GetPaymentRequest DTO creates a new class using paymentId as the payment identifier into a new Square object
      GetPaymentsRequest request = new GetPaymentsRequest
      {
        PaymentId = paymentId
      };

      return _client.Payments.GetAsync(request);
  }
}