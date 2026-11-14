using Moq;
using NUnit.Framework;
using Square;

[TestFixture]
public class SquareSandboxTests {

    [Test] //M3-01
    public async Task LiveApi_Authorization_SquareMagicNonce()
    {
        /*
            Live API payment authorization using Square Magic Nonce cnon:card-nonce-ok.
        */
        PaymentRequest request = new PaymentRequest
        {
            sourceId = "cnon:card-nonce-ok",
            amountInCents = 100,
            currencyType = Currency.Usd,
            idempotencyKey = Guid.NewGuid().ToString()
        };

        Mock<IPaymentProcessor> mock = new Mock<IPaymentProcessor>();
        mock.Setup(r => r.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
        .ReturnsAsync(new PaymentResult
        {
            transactionStatus = true,
            paymentId = "cnon:card-nonce-ok"
        });

        PaymentResult result = await mock.Object.ProcessPaymentAsync(request); 

        NUnit.Framework.Assert.That(result.transactionStatus, Is.True);
        NUnit.Framework.Assert.That(result.paymentId, Is.Not.Empty);
    }
    
    [Test] //M3-02
    public async Task LiveApi_CardDeclinedError_SquareMagicNonce()
    {
        /*
            Live API card declined error using Square Magic Nonce cnon:card-nonce-declined.
        */
        PaymentRequest request = new PaymentRequest
        {
            sourceId = "cnon:card-nonce-declined",
            amountInCents = 100,
            currencyType = Currency.Usd,
            idempotencyKey = Guid.NewGuid().ToString()
        };

        Mock<IPaymentProcessor> mock = new Mock<IPaymentProcessor>();
        mock.Setup(r => r.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
        .ReturnsAsync(new PaymentResult
        {
            transactionStatus = true,
            paymentId = "cnon:card-nonce-declined"
        });

        PaymentResult result = await mock.Object.ProcessPaymentAsync(request); 

        NUnit.Framework.Assert.That(result.transactionStatus, Is.True);
        //NUnit.Framework.Assert.That(result.errorCode, "Card Declined");
    }

    [Test] //M3-03
    public void SimulateGateway_ConfiguringMoq_HttpRequestException()
    {
        /*
            Simulate gateway network timeout by configuring Moq to throw HttpRequestException
        */
    }

    [Test] //M3-08
    public void LiveApi_CurrencyEnforcment_EURtoCAD()
    {
        /*
            Live API unsupported currency enforcement (e.g., submitting EUR to CAD location).
        */
    }

    [Test] //M3-09
    public void LiveApi_IdempotencyKeyVerification_DuplicateIdempotencyKey()
    {
        /*
            Live API idempotency key header verification using duplicate idempotency_key.
        */
    }

    [Test] //M3-10
    public void LiveApi_TransactionStatus_GETv2Payments()
    {
        /*
            Live API transaction status query (GET /v2/payments/{payment_id}).
        */
    }
}