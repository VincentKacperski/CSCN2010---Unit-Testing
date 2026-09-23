using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculateTests
{
    [TestClass]
    public class CalculateTest
    {
      
      [TestMethod] //M4-17
      public void CalculateShares_EqualSplit_ApportionsEvenly()
      {
        //Arrange

        //Act

        //Assert  
      }

      [TestMethod] //M4-18
      public void CalculateShares_ProportionalSplit_ApportionsWeighted()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M4-19
      public void CalculateShares_ExcludedAttendee_ApportionsZero()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M4-20
      public void CalculateShares_TaxAndPercentTip_ApportionsTotal()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M4-21
      public void CalculateShares_TaxAndFixedTip_ApportionsTotal()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M4-22
      public void CalculateShares_AllAttendeesExcluded_ThrowsException()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M4-23
      public void CalculateShares_TotalWeightsZero_ThrowsException()
      {
        //Arrange

        //Act

        //Assert
      }

    }
}









