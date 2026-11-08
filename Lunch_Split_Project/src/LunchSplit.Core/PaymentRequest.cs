using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace PaymentRequests.Tests;

public class PaymentRequestTests {

    // Test M3-02: Card Declined Exception Simulation
    [TestMethod]
    [ExpectedException(typeof(PaymentDeclinedException))]
    public async Task ProcessPayment_CardDeclined_ThrowsPaymentDeclinedException()
    {
        // Arrange
        var mock = new Mock<IPaymentProcessor>();
        mock.Setup(p => p.ProcessPaymentAsync(It.Is<PaymentRequest>(r => r.CardNonce
        == "cnon:card-nonce-declined"))).ThrowsAsync(new PaymentDeclinedException("Card declined:INSUFFICIENT_FUNDS"));
        var service = new SquarePaymentService(mock.Object);

        // Act
        await service.ExecuteTransactionAsync("cnon:card-nonce-declined", 50.00m);
    }

}

/*
Domain-specific models representing monetary amount,
currency code (e.g. 'CAD'), idempotency key, card nonce,
and resulting transaction IDs/error codes.
*/