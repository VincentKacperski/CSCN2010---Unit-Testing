using Moq;
using NUnit.Framework;
using Square;
using Square.Payments;

[TestFixture]
public class SquareSandboxTests {

    private Mock<ISquarePaymentsGateway> _gatewayMock;
    private SquarePaymentService _service;

    [SetUp] 
    public void Setup()
    {
        //Set up a new gateway mock object
        _gatewayMock =
            new Mock<ISquarePaymentsGateway>();

        //Create the new gatway mock object
        _service =
            new SquarePaymentService(_gatewayMock.Object);
    }

    [Test] //M3-01
    public async Task LiveApi_Authorization_SquareMagicNonce()
    {
        //Arrange

        //Create a new payment request
        PaymentRequest request = new PaymentRequest
        {
            sourceId = "cnon:card-nonce-ok",
            amountInCents = 1000,
            currencyType = Currency.Usd,
            idempotencyKey = Guid.NewGuid().ToString()
        };

        //Create a new payment response
        var response = new CreatePaymentResponse
        {
            Payment = new Payment
            {
                Id = "payment-001",
                Status = "Completed",
                AmountMoney = new Money
                {
                    Amount = 1000,
                    Currency = Currency.Usd,
                }   
            }
        };

        //Set upp the mock text to create an asyncronous payment
        _gatewayMock.Setup(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>())).ReturnsAsync(response);

        var paymentStatusResponse = new GetPaymentResponse
        {
            Payment = new Payment
            {
                Id = "payment-001",
                Status = "COMPLETED",
                AmountMoney = new Money
                {
                    Amount = 1000,
                    Currency = Currency.Usd
                }
            }
        };

        //Set up the mock text to get the asyncronous payment
        _gatewayMock
            .Setup(x => x.GetPaymentAsync("payment-001"))
            .ReturnsAsync(paymentStatusResponse);

        //Act
        var result = await _service.ProcessPaymentAsync(request);

        //Assert
        NUnit.Framework.Assert.That(result.transactionStatus, Is.True);
        NUnit.Framework.Assert.That(result.paymentId, Is.Not.Null.And.Not.Empty);

    }
    
    [Test] //M3-02
    public async Task LiveApi_CardDeclinedError_SquareMagicNonce()
    {
        //Test being rebuilt
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