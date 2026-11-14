public class PaymentRefundRequest
{
  public string? paymentId {get; set;}
  public long amount {get; set;}
  public string? currencyType {get;set;}
  public string? idempotencyKey {get;set;}
  public string? Reason {get;set;}

}