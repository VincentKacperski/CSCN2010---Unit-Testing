using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace SquareSandBox.Tests;

[TestClass]
public class SquareSandboxTests {

    [TestMethod] //M3-01
    public void LiveApi_Authorization_SquareMagicNonce()
    {
        /*
            Live API payment authorization using Square Magic Nonce cnon:card-nonce-ok.
        */
    }
    
    [TestMethod] //M3-02
    public void LiveApi_CardDeclinedError_SquareMagicNonce()
    {
        /*
            Live API card declined error using Square Magic Nonce cnon:card-nonce-declined.
        */
    }

    [TestMethod] //M3-03
    public void SimulateGateway_ConfiguringMoq_HttpRequestException()
    {
        /*
            Simulate gateway network timeout by configuring Moq to throw HttpRequestException
        */
    }

    [TestMethod] //M3-08
    public void LiveApi_CurrencyEnforcment_EURtoCAD()
    {
        /*
            Live API unsupported currency enforcement (e.g., submitting EUR to CAD location).
        */
    }

    [TestMethod] //M3-09
    public void LiveApi_IdempotencyKeyVerification_DuplicateIdempotencyKey()
    {
        /*
            Live API idempotency key header verification using duplicate idempotency_key.
        */
    }

    [TestMethod] //M3-10
    public void LiveApi_TransactionStatus_GETv2Payments()
    {
        /*
            Live API transaction status query (GET /v2/payments/{payment_id}).
        */
    }
}