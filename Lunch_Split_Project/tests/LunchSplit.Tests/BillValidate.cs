using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Validate.Tests
{
    [TestClass]
    public class ValidateTest
    {
      
      [TestMethod] //M3-12
      public void Validate_CompleteBillDetails_ReturnsOk()
      {
        //Arrange

        //Act

        //Assert  
      }

      [TestMethod] 
      public void Validate_EmptyAttendeeCollection_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] 
      public void Validate_NegativeSubtotal_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] 
      public void Validate_NegativeTax_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] 
      public void Validate_ZeroAttendees_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

    }
}







