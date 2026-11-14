public class PaymentRequest {

    //Get the payment source card (payment_nonce)
    public string? sourceId {get; set;} = string.Empty;
    //Represnets a currency in total cents
    public int amountInCents {get; set;}
    //Three lets ISO 4217 currency code
    public Square.Currency currencyType {get; set;}
    //Unique string key to prevent duplicte transactions
    public string? idempotencyKey {get; set;} = string.Empty;
    //Indicates the pysical location or vendor
    public string? locationId {get; set;} = string.Empty;
    //Process fund immediatly or put them on hold
    public string? customerId {get; set;} = string.Empty;
    //Process fund immediatly or put them on hold
    public string? referenceId {get; set;} = string.Empty;
    //Process fund immediatly or put them on hold
    public bool autoComplete {get; set;}

}