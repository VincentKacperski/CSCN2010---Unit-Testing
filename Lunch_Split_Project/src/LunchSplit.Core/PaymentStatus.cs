//Defines the payments statuses that can go through
public class PaymentStatus //Payment Statuses when createing a new PaymentRequest
{
    public string? paymentId {get; set;}
    public string? paymentStatus {get; set;}
    public long amount{get; set;}
    public string? currencyType{get; set;}
    public DateTime? createdAt{get; set;}
    public DateTime? updatedAt{get; set;}

    //When get a payments status it can come as the following ...
    /*
      APPROVED //The payment was successful
      PENDING //The payment is pending
      COMPLETED //The payment was fully completed
      CANCELED //The user canceled the payment
      FAILED  //The payment failed to go through
    */

}