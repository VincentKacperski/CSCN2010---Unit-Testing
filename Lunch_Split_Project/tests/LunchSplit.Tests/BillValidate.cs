using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ValidateTests
{
    [TestClass]
    public class ValidateTest
    {
      
      [TestMethod] //M3-12
      public void Validate_CompleteBillDetails_ReturnsOk()
      {
        //Arrange
        int result = 0;
        Bill bill = new Bill(80, 10, 10, TipMode.none); //Create a new bill object
        List<Attendee> attendees = new List<Attendee>();
        Attendee person1 = new Attendee("Vincent", 3, 2, true);
        Attendee person2 = new Attendee("Jake", 1, 2, true);
        attendees.Add(person1);
        attendees.Add(person2);

        //Act
        int error = bill.Validate(bill, attendees);

        //Assert  
        Assert.AreEqual(result, error);
      }

      [TestMethod] //M3-13
      public void Validate_EmptyAttendeeCollection_ReturnsFail()
      {
        //Arrange
        int result = -1;
        Bill bill = new Bill(80, 10, 10, TipMode.none); //Create a new bill object
        List<Attendee> attendees = new List<Attendee>();

        //Act
        int error = bill.Validate(bill, attendees);

        //Assert  
        Assert.AreEqual(result, error);
      }

      [TestMethod] //M3-14
      public void Validate_NegativeSubtotal_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-15
      public void Validate_NegativeTax_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

      [TestMethod] //M3-16
      public void Validate_ZeroAttendees_ReturnsFail()
      {
        //Arrange

        //Act

        //Assert
      }

    }
}







