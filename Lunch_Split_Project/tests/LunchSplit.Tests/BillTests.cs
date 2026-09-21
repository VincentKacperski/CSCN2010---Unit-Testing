using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bill.Tests
{
    [TestClass]
    public class BillTest
    {
        [TestMethod] //M1-01
        public void ComputeTip_NoTipMode_ReturnsZero()
        {
            //Arrange
            decimal billTotal = 100.00m;
            TipMode tm = TipMode.none;
            decimal expectedTip = 0.0m;
            Bill bill = new Bill(); //Create a new bill object

            //Act
            decimal actualResult = bill.calculateTip(billTotal, tm);
            //Assert
            Assert.AreEqual(expectedTip, actualResult);
        }

        [TestMethod] //M1-02
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            //Arrange
            decimal billTotal = 120.00m;
            decimal percentage = 0.15m;
            decimal expectedValue = 18.00m;
            TipMode tipMode = TipMode.percent;
            Bill bill = new Bill(); //Create a new bill object

            //Act
            decimal actualResult = bill.calculateTip(billTotal, tipMode, percentage); 

            //Assert
            Assert.AreEqual(expectedValue, actualResult);
        }

        [TestMethod] //M1-03
        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            //Arrange
            decimal billTotal = 100.00m;
            TipMode tm = TipMode.fixed_tip;
            decimal percentage = 0.15m;
            decimal expectedValue = 15.00m;
            Bill bill = new Bill(); //Create a new bill object

            //Act
            decimal actualResult = bill.calculateTip(billTotal, tm, percentage);

            //Assert
            Assert.AreEqual(expectedValue, actualResult);
        }

        [TestMethod] //M1-04
        public void ComputeTip_NegativeSubtotal_ThrowsException()
        {
            //Arrange
            decimal billTotal = -50; //Tip of -50
            TipMode tm = TipMode.fixed_tip;
            decimal fixedValue = -5.00m;
            decimal expectedValue = 1; //If 1 an ArgumentOutOfRangeException was thrown
            Bill bill = new Bill(); //Create a new bill object

            //Act
            decimal actualResult = bill.calculateTip(billTotal, tm, fixedValue); //Returns either a 1 or 0 from a try catch;

            //Assert
            Assert.AreEqual(expectedValue, actualResult); //Expect 1 as the actual result
        }

        [TestMethod] //M1-04
        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
            //Arrange
            decimal billTotal = -30; //Tip of 30
            TipMode tm = TipMode.percent;
            decimal percent = 0.10m;
            decimal expectedValue = 1; //If 1 an ArgumentOutOfRangeException was thrown
            Bill bill = new Bill(); //Create a new bill object

            //Act
            decimal actualResult = bill.calculateTip(billTotal, tm, percent); //Returns either a 1 or 0 from a try catch;

            //Assert
            Assert.AreEqual(expectedValue, actualResult); //Expect 1 as the actual result
        }
    }
} 