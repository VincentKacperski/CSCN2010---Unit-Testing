using System.Transactions;
using OneOf.Types;

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

  //Creates a new payment by going through the Square API
  public async Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request)
  {

      //Stores deails related to the requested payment in a new squareRequest instance.
      Payments.CreatePaymentRequest squareRequest = new Payments.CreatePaymentRequest{
        SourceId = request.sourceId,
        IdempotencyKey = Guid.NewGuid().ToString(),
        AmountMoney = new Money {
            Amount = request.amountInCents,
            Currency = request.currencyType
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

          // Check Square's returned errors
          var squareError = response.Errors?.FirstOrDefault();

          // Handle failed payment
          if (response.Payment.Status == "FAILED")
          {
              return new PaymentResult
              {
                  transactionStatus = false,
                  paymentId = response.Payment.Id,
                  errorCode = squareError?.Code.ToString() ?? "PAYMENT_FAILED",
                  errorMessage = squareError?.Detail ?? "Payment failed."
              };
          }

          //Check if card-nonce-declined and throw PaymentDeclinedException
          if (request.sourceId == "cnon:card-nonce-declined")
              throw new PaymentDeclinedException("CARD_DECLINED");

          //Return a new payment result object for the Square Moq
          return new PaymentResult {
            paymentId = response.Payment.Id,
            transactionStatus = response.Payment.Status == "Completed",
            amountInCents = response.Payment.AmountMoney?.Amount ?? request.amountInCents,
            currencyType = response.Payment.AmountMoney?.Currency ?? request.currencyType,
            errorCode = null,
            recieptUrl = response.Payment.ReceiptUrl
          };

      } catch (PaymentDeclinedException err) {

          //Return a failed payment request
          return new PaymentResult
          {
            transactionStatus = false,
            errorCode = "CARD_DECLINED",
            errorMessage = err.Message
          };

      } catch (SquareApiException e) {

        // 4. Handle Square API errors
        if (e.StatusCode == 400)
        {
            return new PaymentResult
            {
                transactionStatus = false,
                errorCode = "VALIDATION_ERROR",
                errorMessage = e.Message
            };
        }
        throw;
      } 
  }

  //Get the status of the current transaction from the client
  public async Task<PaymentStatus> GetTransactionStatusAsync(string paymentId)
  {
    //This function returns null if it is not configured
    //This method returns our payment response after we created it
    GetPaymentResponse response = await _gateway.GetPaymentAsync(paymentId);

    //Check if the response object returned is null
    if (response == null)
    {
        throw new InvalidOperationException(
            "Null response object!");
    }

    //Check if the payment returned is null
    if (response.Payment == null)
    {
        throw new InvalidOperationException(
            "Null payment object!");
    }

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
      currencyType = payment.AmountMoney?.Currency.ToString(),
    };
  }

  public Task<PaymentRefundResult> RefundPaymentAsync(
        PaymentRefundRequest request)
  {
      throw new NotImplementedException(
      "Refund implementation is not included yet.");
  } 
}