using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharesTests
{
    [TestClass]
    public class ShareTest
    {
        [TestMethod] //M2-06
        public void ComputeTip_NegativeFixedTip_ThrowsException()
        {
            //Arrange

            //Act

            //Assert
        }

        [TestMethod] //M2-07
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {
            //Arrange

            //Act

            //Assert            
        }

        [TestMethod] //M2-08
        public void RoundShares_BankersRounding_RoundsToNearestEven() 
        {
            //Arrange

            //Act

            //Assert  
        }

        [TestMethod] //M2-09
        public void RoundShares_RoundUp_AppliesCeilingToCents()
        {
            //Arrange

            //Act

            //Assert  
        }

        [TestMethod] //M2-10
        public void RoundShares_UnevenSplit_ReconcilesRemainder()
        {
            //Arrange

            //Act

            //Assert
        }
        
        [TestMethod] //M2-11
        public void RoundShares_EmptyShareCollection_ReturnsEmpty()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}