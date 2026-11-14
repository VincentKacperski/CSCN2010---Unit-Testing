using System.Transactions;

namespace Square;
public class SquarePaymentService : IPaymentProcessor
{
  //Declare a readonly square client instance
  private readonly ISquarePaymentsGateway _gateway;

  //Constructor to get the current transaction client object
  public SquarePaymentService(ISquarePaymentsGateway gateway)
  {
    _gateway = gateway;
  }

  public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
  {
      //Check if the card nonce has been declined
      if(request.sourceId == "cnon:card-nonce-declined")
      {
        throw new PaymentDeclinedException (
          "Card declined: INSUFFICIENT_FUNDS" //Throw a PaymentDeclinedException
        );
      }

      //Check if the payment request is null 
      if (request.amountInCents <= 0)
          throw new ArgumentNullException("Payment cannot be 0."); //Throw and argument null exception

       //Check if the bill total is 0
      if (string.IsNullOrWhiteSpace(request.idempotencyKey))
          throw new ArgumentNullException("Idempotency Key is required."); //Throw a ArgumentNullException

      Payments.CreatePaymentRequest squareRequest = new Payments.CreatePaymentRequest{
        SourceId = request.sourceId,
        IdempotencyKey = Guid.NewGuid().ToString(),
        AmountMoney = new Money {
            Amount = request.amountInCents,
            Currency = Currency.Usd
        },
        LocationId = request.locationId,
        CustomerId = request.customerId,
        ReferenceId = request.referenceId,
        Autocomplete = true 
      };

      try { 
          //Backend sends a request to Square calling CreateAsync
          //Square retuns a result with all payment details
          var response = await _gateway.CreatePaymentAsync(squareRequest);

          //Check if a payment was created sucessfully before checking id
          if (response?.Payment == null)
          {
             throw new InvalidOperationException("No payment was made.");
          }

          //Check if the payment recieved a valid id if the payment is not null
          if (string.IsNullOrWhiteSpace(response.Payment.Id))
          {
             throw new InvalidOperationException("No payment Id found.");
          }

          //Pass the responses payment Id to get the transaction status 
          await GetTransactionStatusAsync(response.Payment.Id);

          //Return a new payment result object for the Square Moq
          return new PaymentResult {
            paymentId = response.Payment.Id,
            transactionStatus = true,
            amountInCents = response.Payment.AmountMoney?.Amount ?? request.amountInCents,
            currencyType = response.Payment.AmountMoney?.Currency ?? request.currencyType,
            errorCode = null,
            recieptUrl = response.Payment.ReceiptUrl
          };

      } catch (SquareApiException error_message) {

          //return a failed result for the payment Moq
          return new PaymentResult {
            paymentId = null,
            transactionStatus = false,
            amountInCents = request.amountInCents,
            errorCode = error_message.Errors.FirstOrDefault()?.Code.ToString(),
            errorMessage = error_message.Message
          };
      }
  }

  //Get the status of the current transaction from the client
  public async Task<PaymentStatus> GetTransactionStatusAsync(string paymentId)
  {
    GetPaymentResponse response = await _gateway.GetPaymentAsync(paymentId);

    //Get payment details from response
    Payment payment = response.Payment;

    //check if response returned a non null payment
    if (payment == null)
    {
      throw new InvalidOperationException(
        "Square returned no payment.");
    }

    return new PaymentStatus
    {
      paymentId = payment.Id,
      paymentStatus = payment.Status?.ToString(),
      amount = payment.AmountMoney?.Amount ?? 0,
      currencyType = payment.AmountMoney?.Currency?.ToString(),
      createdAt = DateTime.Parse(payment.CreatedAt),
      updatedAt = DateTime.Parse(payment.UpdatedAt)
    };
  }

  public Task<PaymentRefundResult> RefundPaymentAsync(
        PaymentRefundRequest request)
  {
      throw new NotImplementedException(
      "Refund implementation is not included yet.");
  } 
}