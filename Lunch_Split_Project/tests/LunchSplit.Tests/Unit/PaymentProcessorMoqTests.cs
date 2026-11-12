using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace PaymentProcessorMoq.Tests;

[TestClass]
public class PaymentProcessorMoqTests {

    [TestMethod] //M3-03
    public void SimulateGatewayNetwork_ConfiguringMoq_ThrowHttpExcpetion()
    {
       /*
         Simulate gateway network timeout by configuring Moq to throw HttpRequestException.
       */
    }

    [TestMethod] //M3-04
    public void ParameterInvocation_ProcessPaymentAsync_MoqVerify()
    {
       /*
         Parameter & invocation verification on ProcessPaymentAsync using Mock.Verify().
       */
    }

    [TestMethod] //M3-05
    public void PreconditionShortCircut_ZeroBill_PassedPaymentManager()
    {
       /*
         Precondition short-circuit: $0.00 bill request passed to payment manager.
       */
    }

    [TestMethod] //M3-06
    public void BatchPayment_Iteration_FourAttendeeSplit()
    {
       /*
         Batch payment iteration across a 4-attendee split bill.
       */
    }

}
