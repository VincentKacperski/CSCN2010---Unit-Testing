using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace SquarePaymentService.Tests;

public class SquarePaymentService {

    [Test]
    public void FirstTest()
    {
       //Do nothing for now
    }

}

/*
Implements IPaymentProcessor by translating domain
DTOs into Square API SDK payloads (PaymentsApi.CreatePaymentAsync)
and executing gateway requests in Sandbox mode.
*/