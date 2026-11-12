using Lunch_Split_Split.Payments;
public interface IPaymentProcessor {
  Task<PaymentResult> ProcessPaymentAsync(PaymentRequest request); 
  Task<PaymentStatus> GetTransactionStatusAsync(string transactionId);
}

