using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BillTests
{
    [TestClass]
    public class BillTest
    {
        [TestMethod] //M1-01
        public void ComputeTip_NoTipMode_ReturnsZero()
        {
            //Arrange
            decimal billTotal= 0;
            decimal tip_input = 0;
            decimal expectedTip = 0;
            TipMode tm;
            Bill bill = new Bill(95, 5, 0, TipMode.none); //Create a new bill object

            //Act
            billTotal = bill.getSubTotal() + bill.getTax();
            tip_input = bill.getTip();
            tm = bill.getTipMode();
            decimal actualResult = bill.calculateTip(billTotal, tm, tip_input);

            //Assert
            Assert.AreEqual(expectedTip, actualResult);
        }

        [TestMethod] //M1-02
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            //Arrange
            decimal billTotal = 0;
            decimal tip_input = 0;
            decimal expectedTip = 18;
            TipMode tm;
            Bill bill = new Bill(110, 10, 0.15m, TipMode.percent); //Create a new bill object

            //Act
            billTotal = bill.getSubTotal() + bill.getTax();
            tip_input = bill.getTip();
            tm = bill.getTipMode();
            decimal actualResult = bill.calculateTip(billTotal, tm, tip_input);

            //Assert
            Assert.AreEqual(expectedTip, actualResult);
        }

        [TestMethod] //M1-03
        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            //Arrange
            decimal billTotal = 0;
            decimal tip_input = 0;
            TipMode tm;
            Bill bill = new Bill(80, 5, -15m, TipMode.percent); //Create a new bill object

            //Act
            billTotal = bill.getSubTotal() + bill.getTax();
            tip_input = bill.getTip();
            tm = bill.getTipMode();

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bill.calculateTip(billTotal, tm, tip_input));

        }

        [TestMethod] //M1-04
        public void ComputeTip_NegativeSubtotal_ThrowsException()
        {
            //Arrange
            decimal billTotal = 0;
            decimal tip_input = 0;
            TipMode tm;
            Bill bill = new Bill(-80, 5, 0.15m, TipMode.percent); //Create a new bill object

            //Act
            billTotal = bill.getSubTotal() + bill.getTax();
            tip_input = bill.getTip();
            tm = bill.getTipMode();

            //Assert
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => bill.calculateTip(billTotal, tm, tip_input));

        }

        [TestMethod] //M1-05
        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
            //Arrange

            //Act

            //Assert
        }
    }
} 