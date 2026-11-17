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
  public async Task<CreatePaymentResponse> CreatePaymentAsync(
    CreatePaymentRequest request)
  {
    try
    {
        return await _client.Payments.CreateAsync(request);
    }
    catch (PaymentDeclinedException e)
    {
        throw new PaymentDeclinedException(e.Message);
    }
  }

  public async Task<GetPaymentResponse> GetPaymentAsync(string paymentId)
  {     

      Console.WriteLine("Entering SquarePaymentsGateway.GetPaymentAsync");
      //GetPaymentRequest DTO creates a new class using paymentId as the payment identifier into a new Square object
      GetPaymentsRequest request = new GetPaymentsRequest
      {
        PaymentId = paymentId
      };

      try
      {
          GetPaymentResponse response = await _client.Payments.GetAsync(request);
          return response;
      }
      catch (Exception ex)
      {
          Console.WriteLine($"Exception: {ex.GetType().FullName}");
          Console.WriteLine($"Message: {ex.Message}");
          throw;
      }

      //return await _client.Payments.GetAsync(request);;
  }
}