namespace Square;
public class SquarePaymentService : IPaymentProcessor
{
  //Declare a readonly square client instance
  private readonly SquareClient _client;

  //Constructor to get the current transaction client object
  public SquarePaymentService(string accessToken)
  {
    //Get the clients application token for payment processing their session url
     _client = new SquareClient(accessToken, new ClientOptions {BaseUrl = "https://developer.squareup.com/us/en"});
  }

  //Get the status of the current transaction from the client
  public async Task<PaymentStatus> GetTransactionStatusAsync(string transactionId)
  {
    //Get a payment response from the the client
    GetPaymentResponse response = await _client.Payments.GetAsync(new Payments.GetPaymentsRequest
    {
      PaymentId = transactionId //Tells square wich payment Id to recieve
    });

    Payment? payment = response.Payment;
    switch(payment?.Status) 
    {
      case "Pending": 
        return PaymentStatus.Pending; //Return enum PaymentStatus as 5 
      case "Approved":
        return PaymentStatus.Approved; //Return enum PaymentStatus as 4
      case "Completed":
        return PaymentStatus.Completed; //Return enum PaymentStatus as 3  
      case "cancelled":
        return PaymentStatus.Cancelled; //Return enum PaymentStatus as 2
      case "Failed":
        return PaymentStatus.Failed; //Return enum PaymentStatus as 1
      default:
        return PaymentStatus.Unknown; //Returnn enum PaymentStatus as 0
    };
  }

  public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
  {

      if (request == null) //Check if the payment request is null 
      {
        throw new ArgumentNullException(nameof(request)); //Throw and argument null exception
      }

      if (request.amount_money <= 0) //Check if the bill total is 0
      { 
        throw new ArgumentNullException(nameof(request)); //Throw a ArgumentNullException
      }

      //Check if the card nonce has been declined
      if(request.card_nonce == "cnon:card-nonce-declined")
      {
        throw new PaymentDeclinedException (
          "Card declined: INSUFFICIENT_FUNDS" //Throw a PaymentDeclinedException
        );
      }

      Payments.CreatePaymentRequest squareRequest = new Payments.CreatePaymentRequest{
        SourceId = request.source_id,
        IdempotencyKey = Guid.NewGuid().ToString(),
        AmountMoney = new Money {
            Amount = request.amount_money,
            Currency = Currency.Usd
        }
      };

      try { //Code refrenced from Square.Api Repo
          var response = await _client.Payments.CreateAsync(squareRequest);
          var payment = response.Payment;

          return new PaymentResult(
            Success: true,
            PaymentId: payment.Id,
            Status: payment?.Status?.ToString(),
            ErrorMessage: null
          );

      } catch (SquareApiException e) {
          
          //Display the exceptions details
          Console.WriteLine(e.Body);
          Console.WriteLine(e.StatusCode);
          // Access the parsed error objects
          foreach (var error in e.Errors) {
              Console.WriteLine($"category: {error.Category}");
              Console.WriteLine($"code: {error.Code}");
              Console.WriteLine($"detail: {error.Detail}");
              Console.WriteLine($"field: {error.Field}");
          } //Refrenced from Square SDK github Repo

          return new PaymentResult(
            Success: false,
            PaymentId: null,
            Status: null,
            ErrorMessage: e.Message
          );
      }
  }
}