public class PaymentRefundResult
{
  public bool Success {get; set;}
  public string? refundId {get;set;}
  public string? refundStatus {get;set;}
  public string? errorCode {get;set;}
  public string? errorMessage {get;set;}

}