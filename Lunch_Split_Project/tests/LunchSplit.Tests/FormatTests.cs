using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculateTests
{
    [TestClass]
    public class FormatTests
    {
      
      [TestMethod] //M5-24
      public void Format_DefaultBillReceipt_ContainsLineItems()
      {
        //Arrange

        //Act

        //Assert  
      }

      [TestMethod] //M5-25
      public void Format_ReceiptOutput_ContainsRequiredMetadata()
      {
        //Arrange

        //Act

        //Assert
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