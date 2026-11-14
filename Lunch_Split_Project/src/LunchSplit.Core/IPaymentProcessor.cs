public interface IPaymentProcessor {

  //Process a current payment
  Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request); 

  //Get the status of a current transaction
  Task<PaymentStatus> GetTransactionStatusAsync(string transactionId);
  
  //Get the status of a payment refund
  Task<PaymentRefundResult> RefundPaymentAsync(
        PaymentRefundRequest request);
}

