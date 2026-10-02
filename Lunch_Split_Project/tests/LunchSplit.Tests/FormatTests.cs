using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Formatter.FormatReceipt
{
    [TestClass]
    public class FormatTests
    {
      
      [TestMethod] //M5-24
      public void Format_DefaultBillReceipt_ContainsLineItems()
      {
        //Arrange
        List<Share> shares = new List<Share>();
        Share share1 = new Share("Julia", 50);
        Share share2 = new Share("John", 50);
        shares.Add(share1); shares.Add(share2);
        List<Attendee> attendees = new List<Attendee>();
        Attendee attendee1 = new Attendee("Julia", 1, 2, true);
        Attendee attendee2 = new Attendee("John", 1, 1, true);
        attendees.Add(attendee1); attendees.Add(attendee2);
        Bill bill = new Bill(100, 12, 10, TipMode.fixed_tip);

        //Act
        String receipt = bill.Format(bill, attendees, shares);

        //Assert
        StringAssert.Contains(receipt, "SubTotal: 100");
        StringAssert.Contains(receipt, "Tax: 12");  
        StringAssert.Contains(receipt, "Tip: 10"); 
        StringAssert.Contains(receipt, "Julia: 50");
        StringAssert.Contains(receipt, "John: 50");  

      }

      [TestMethod] //M5-25
      public void Format_ReceiptOutput_ContainsRequiredMetadata()
      {
          //Arrange
          List<Share> shares = new List<Share>();
          Share share1 = new Share("Julia", 50);
          Share share2 = new Share("John", 50);
          shares.Add(share1); shares.Add(share2);
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Julia", 1, 2, true);
          Attendee attendee2 = new Attendee("John", 1, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2);
          Bill bill = new Bill(100, 12, 10, TipMode.fixed_tip);

          //Act
          String receipt = bill.Format(bill, attendees, shares);

          //Assert
          StringAssert.Contains(receipt, "Vincent Kacperski");
          StringAssert.Contains(receipt, "2026-10-02");  
          StringAssert.Contains(receipt, "6:14pm"); 
      }

      [TestMethod] //TDD-26
      public void Format_CsvReceiptExporter_MatchesSchema()
      {
        //Blank for now
      }

      [TestMethod] //TDD-27
      public void CalculateShares_PaymentRequest_ValidatesStripeMock()
      {
        //Blank for now
      }

    }
}