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

            //Act

            //Assert  
        }

        [TestMethod] //M1-02
        public void ComputeTip_PercentTipMode_ReturnsCorrectPercent()
        {
            //Arrange

            //Act

            //Assert  
        }

        [TestMethod] //M1-03
        public void ComputeTip_FixedTipMode_ReturnsFixedAmount()
        {
            //Arrange

            //Act

            //Assert  
        }

        [TestMethod] //M1-04
        public void ComputeTip_NegativeSubtotal_ThrowsException()
        {
            //Arrange

            //Act

            //Assert  
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