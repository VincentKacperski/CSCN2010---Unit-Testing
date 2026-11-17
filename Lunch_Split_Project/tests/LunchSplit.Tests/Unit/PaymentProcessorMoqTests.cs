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
      PaymentRequest request = new PaymentRequest
      {
          sourceId = "cnon:card-nonce-ok",
          amountInCents = 0,
          currencyType = Currency.Cad,
          idempotencyKey = Guid.NewGuid().ToString()
      };

      try
      {
        await _service.ProcessPaymentAsync(request);
      }
      catch (ArgumentException ex)
      {
        NUnit.Framework.Assert.That(ex.Message,
        Does.Contain("Payment amount must be greater than zero."));
      }

      // Verify that the gateway was never called
      _gatewayMock.Verify(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>()), Times.Never);
    }

    //M3-06
    public async Task BatchPayment_Iteration_FourAttendeeSplit()
    {
      /*
        Batch payment iteration across a 4-attendee split bill.
      */
      
      //Arrange
      PaymentRequest request = new PaymentRequest
      {
          sourceId = "cnon:card-nonce-ok",
          amountInCents = 1000,
          currencyType = Currency.Cad,
          idempotencyKey = Guid.NewGuid().ToString()
      };

      CreatePaymentResponse paymentResponse = new CreatePaymentResponse
      {
        Payment = new Payment
        {
            Id = "moq-006",
            Status = "Completed"
        }
      };

      _gatewayMock.Setup(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>())).ReturnsAsync(paymentResponse);

      // Act 
      for (int i = 0; i<3; i++) {
        PaymentResult result = await _service.ProcessPaymentAsync(request);
      }

      //Assert
      _gatewayMock.Verify(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>()), Times.Exactly(4));
    }

    //M3-07
    public async Task BatchFailure_TwoSucced_ThreeAttendeeSplit()
    {
      /*
         Partial batch failure resilliance: attendee #2 declines while #1 and #3 succeed 
      */

      // Arrange

      //Create a array of payment statuses that each unique payment request will be given
      string[] transactionStatuses = {"Completed","Failed","Completed"};
      
      //Create an array of payment results using each status
      CreatePaymentResponse[] paymentResults = transactionStatuses.Select((status, index) => new CreatePaymentResponse
      {
        Payment = new Payment
        {
            Id = Guid.NewGuid().ToString(), //Create a new generated user id
            Status = status //Assign a unique payment status
        }
      }).ToArray(); 

      int counter = 0; //Payment counter to count each newly created payment Moq
      List<PaymentResult> totalResults = new List<PaymentResult>();
      _gatewayMock.Setup(x => x.CreatePaymentAsync(It.IsAny<CreatePaymentRequest>())).ReturnsAsync(() => paymentResults[counter++]);

      // Act 
      for (int i = 0; i<2; i++) {
        PaymentRequest request = new PaymentRequest //Create a new payment request for each attendee
        {
          sourceId = "cnon:card-nonce-ok",
          amountInCents = 1000,
          currencyType = Currency.Cad,
          idempotencyKey = Guid.NewGuid().ToString()
        };
        totalResults.Add(await _service.ProcessPaymentAsync(request)); //Add each payment result to a list of results
      }
    
      //Assert
      NUnit.Framework.Assert.That(totalResults.Count, Is.EqualTo(3));
      NUnit.Framework.Assert.That(totalResults.Count(x => x.transactionStatus), Is.EqualTo(2));
      NUnit.Framework.Assert.That(totalResults.Count(x => !x.transactionStatus), Is.EqualTo(1));

    }
  }
