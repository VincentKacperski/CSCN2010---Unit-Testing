using Square;

public sealed record PaymentRequest (
    string idempotency_key,
    string source_id,
    bool accept_partial_authorization,
    int monetary_amount,
    int app_fee_money,
    bool autoComplete,
    Address billing_address,
    string buyer_email_address
);

//Defines the payments statuses that can go through
public enum PaymentStatus
{
    Unknown, //Payment was unknown
    Failed, //Payment has failed
    Cancelled, //Payment has been cancelled
    Completed, //Payment has been completed
    Approved, //Payment is approved  
    Pending, //Payment is pending
  
}