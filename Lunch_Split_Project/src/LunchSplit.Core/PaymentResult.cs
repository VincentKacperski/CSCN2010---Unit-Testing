public class PaymentResult {

    //Get the payment source card (payment_nonce)
    public string? paymentId {get; set;} = string.Empty;
    //Represents the status of the payment
    public bool transactionStatus {get; set;}
    //Represents a currency in total cents
    public long amountInCents {get; set;}
    //Unique string key to prevent duplicte transactions
    public Square.Currency currencyType {get; set;}
    //Unique string key to prevent duplicte transactions
    public string? recieptUrl {get; set;}
    //Indicated the error code of the transaction
    public string? errorCode {get; set;}
    //Indicates the pysical location or vendor
    public string? errorMessage {get; set;}
    //Process fund immediatly or put them on hold

}