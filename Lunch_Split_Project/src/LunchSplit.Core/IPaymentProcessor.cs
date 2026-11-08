using Moq;
using Moq.Protected;
using NUnit.Framework;

/*
Implements IPaymentProcessor by translating domain DTOs 
into Square API SDK payloads (PaymentsApi.CreatePaymentAsync)
and executing gateway requests in Sandbox mode.
*/

namespace IPaymentProcessor.Tests;
public class IPlaymentProcessorTests {

    [TestMethod]
    public async Task ProcessSharesAsync_ValidPayment_ReturnsSuccessResult()
    {
        // Arrange
        var paymentMock = new Mock<IPaymentProcessor>();
        paymentMock.Setup(p => p.ProcessPaymentAsync(It.IsAny<PaymentRequest>()))
        .ReturnsAsync(new PaymentResult { Success = true, TransactionId = "TX_999" });
        var coordinator = new PaymentCoordinator(paymentMock.Object);

        // Act
        var result = await coordinator.PayShareAsync("Alice", 25.50m);

        // Assert
        Assert.IsTrue(result.IsPaid);
        Assert.AreEqual("TX_999", result.TransactionId);
    }

}



