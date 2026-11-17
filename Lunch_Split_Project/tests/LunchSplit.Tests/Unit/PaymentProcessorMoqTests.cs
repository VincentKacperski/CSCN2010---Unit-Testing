using Moq;
using NUnit.Framework;
using Square;
using Square.Payments;
using System.Net.Http;

  [TestFixture]
  public class PaymentProcessorMoqTests {

    private Mock<ISquarePaymentsGateway> _gatewayMock = null!;
    private SquarePaymentService _service = null!;

    [SetUp]
    public void Setup()
    {
      //Set up a new gateway mock object
      _gatewayMock = new Mock<ISquarePaymentsGateway>();

      //Create the new gatway mock object
      _service = new SquarePaymentService(_gatewayMock.Object);

    }

    [Test] //M3-03
    public async Task SimulateGatewayNetwork_ConfiguringMoq_ThrowHttpExcpetion()
    {
        /*
          Simulate gateway network timeout by configuring Moq to throw HttpRequestException.
        */
        PaymentRequest request = new PaymentRequest
        {
            sourceId = "cnon:card-nonce-ok",
            amountInCents = 1000,
            currencyType = Currency.Cad,
            idempotencyKey = Guid.NewGuid().ToString()
        };

        //Set up a gateway mock, a new payment request that throws an HttpRequestException
        _gatewayMock
        .Setup(x => x.CreatePaymentAsync(
            It.IsAny<CreatePaymentRequest>()))
        .ThrowsAsync(new HttpRequestException(
            "The request timed out while contacting Square."));

        // Act & Assert
        HttpRequestException? exception = await NUnit.Framework.Assert.ThrowsAsync<HttpRequestException>(
        async () => await _service.ProcessPaymentAsync(request));

        _gatewayMock.Verify(x => x.CreatePaymentAsync(
        It.IsAny<CreatePaymentRequest>()), Times.Once);
    }

    [Test] //M3-04
    public async Task ParameterInvocation_ProcessPaymentAsync_MoqVerify()
    {
      /*
        Parameter & invocation verification on ProcessPaymentAsync using Mock.Verify().
      */
      PaymentRequest request = new PaymentRequest
      {
          sourceId = "cnon:card-nonce-ok",
          amountInCents = 3578,
          currencyType = Currency.Cad,
          idempotencyKey = Guid.NewGuid().ToString()
      };

      CreatePaymentResponse paymentResponse = new CreatePaymentResponse
      {
        Payment = new Payment
        {
            Id = "moq-001",
            Status = "Completed"
        }
      };

      //Set up a gateway mock, a new payment request that throws an HttpRequestException
      //Set upp the mock text to create an asyncronous payment
      _gatewayMock.Setup(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>())).ReturnsAsync(paymentResponse);

      // Act 
      PaymentResult result = await _service.ProcessPaymentAsync(request); //Return a payment result

      // Assert
      NUnit.Framework.Assert.That(result.transactionStatus, Is.True); //Check transaction status
      _gatewayMock.Verify(x => x.CreatePaymentAsync(It.Is<CreatePaymentRequest>(p =>
      p.SourceId == request.sourceId && p.AmountMoney != null && p.AmountMoney.Amount == request.amountInCents &&
      p.AmountMoney.Currency == request.currencyType && p.IdempotencyKey == request.idempotencyKey)), Times.Once);
    }

    [Test] //M3-05
    public async Task PreconditionShortCircut_ZeroBill_PassedPaymentManager()
    {
      /*
        Precondition short-circuit: $0.00 bill request passed to payment manager.
      */
    }

    //M3-06
    public async Task BatchPayment_Iteration_FourAttendeeSplit()
    {
      /*
        Batch payment iteration across a 4-attendee split bill.
      */
    }

    //M3-07
    public async Task BatchFailure_TwoSucced_ThreeAttendeeSplit()
    {
      /*
         Partial batch failure resilliance: attendee #2 declines while #1 and #3 succeed 
      */
    }
  }
