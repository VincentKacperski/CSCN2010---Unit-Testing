using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculate.Tests
{
    [TestClass]
    public class CalculateTest
    {
      
      [TestMethod] //M3-17
      public void CalculateShares_EqualSplit_ApportionsEvenly()
      {
        //Arrange

        //Act

        //Assert  
      }

      [TestMethod] //M3-18
      public void CalculateShares_ProportionalSplit_ApportionsWeighted()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-19
      public void CalculateShares_ExcludedAttendee_ApportionsZero()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-20
      public void CalculateShares_TaxAndPercentTip_ApportionsTotal()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-21
      public void CalculateShares_TaxAndFixedTip_ApportionsTotal()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-22
      public void CalculateShares_AllAttendeesExcluded_ThrowsException()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-23
      public void CalculateShares_TotalWeightsZero_ThrowsException()
      {
        //Arrange

        //Act

        //Assert
      }

    }
}









