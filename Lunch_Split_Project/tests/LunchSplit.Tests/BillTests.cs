using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bill.Tests
{
    [TestClass]
    public class BillTest
    {
        [TestMethod]
        public void ComputeTip_NoTipMode_ReturnsZero()
        {
           // arrange
            decimal billTotal = 100.00m;
            TipMode tm = TipMode.none;
            decimal expectedTip = 0.0m;

            Bill bill = new Bill();

            // act
            decimal actualResult = bill.calculateTip(billTotal, tm);
            // assert
            Assert.AreEqual(expectedTip, actualResult);
        }

        [TestMethod]
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            decimal billTotal = 120.00m;
            decimal percentage = 0.15m;
            decimal expectedValue = 18.00m;
            TipMode tipMode = TipMode.percent;

            Bill bill = new Bill();

            decimal actualResult = bill.calculateTip(billTotal, tipMode, percentage); 

            Assert.AreEqual(expectedValue, actualResult);
        }

        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            decimal billTotal = 100.00m;
            TipMode tm = TipMode.fixed_tip;
            decimal fixedValue = 15.00m;
            decimal expectedValue = 15.00m;
            Bill bill = new Bill();

            decimal actualResult = bill.calculateTip(billTotal, tm, fixedValue);
            Assert.AreEqual(expectedValue, actualResult);
        }

    }
} 