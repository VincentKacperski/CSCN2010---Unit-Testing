public record PaymentRequest (
    string source_id,
    string idempotency_key,
    int amount_money,
    string card_nonce
);

//Defines the payments statuses that can go through
public enum PaymentStatus //Payment Statuses when createing a new PaymentRequest
{
    Unknown, //Payment was unknown
    Failed, //Payment has failed
    Cancelled, //Payment has been cancelled
    Completed, //Payment has been completed
    Approved, //Payment is approved  
    Pending, //Payment is pending
  
}