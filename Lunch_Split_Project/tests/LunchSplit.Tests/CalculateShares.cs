using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Splitter.CalculateShares
{
    [TestClass]
    public class CalculateSharesTests
    {
      
      [TestMethod] //M4-17
      public void CalculateShares_EqualSplit_ApportionsEvenly()
      {
          //Arrange
          Bill bill = new Bill(90, 10, 5, TipMode.fixed_tip);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sheldon", 30.00m);
          Share share2 = new Share("Jake", 30.00m);
          Share share3 = new Share("Maria", 30.00m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Jake", 1, 1, true);
          Attendee attendee2 = new Attendee("Sheldon", 1, 1, true);    
          Attendee attendee3 = new Attendee("Maria", 1, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3); 
          Share testShare = new Share(); 

          //Act
          decimal calculatedTip = bill.calculateTip(bill.getTip(), TipMode.fixed_tip, bill.getTip());
          bill.setTip(calculatedTip);
          List<Share> calculatedShares = testShare.CalculateShares(bill, attendees, RoundingMode.up);

          //Assert
          Assert.AreEqual(expectedShares[0].getAmount(), calculatedShares[0].getAmount());
          Assert.AreEqual(expectedShares[1].getAmount(), calculatedShares[1].getAmount());
          Assert.AreEqual(expectedShares[2].getAmount(), calculatedShares[2].getAmount());
      }

      [TestMethod] //M4-18
      public void CalculateShares_ProportionalSplit_ApportionsWeighted()
      {
          //Arrange
          Bill bill = new Bill(90, 20, 5, TipMode.fixed_tip);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Kim", 30m);
          Share share2 = new Share("Jessica", 60m);
          expectedShares.Add(share1); expectedShares.Add(share2); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 1, 1, true);
          Attendee attendee2 = new Attendee("Jessica", 2, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2); 
          Share testShare = new Share(); 

          //Act
          decimal calculatedTip = bill.calculateTip(bill.getSubTotal(), TipMode.fixed_tip, bill.getTip());
          bill.setTip(calculatedTip);
          List<Share> calculatedShares = testShare.CalculateShares(bill, attendees, RoundingMode.up);

          //Assert
          Assert.AreEqual(expectedShares[0].getAmount(), calculatedShares[0].getAmount());
          Assert.AreEqual(expectedShares[1].getAmount(), calculatedShares[1].getAmount());
      }

      [TestMethod] //M4-19
      public void CalculateShares_ExcludedAttendee_ApportionsZero()
      {
          //Arrange
          Bill bill = new Bill(90, 7, 10, TipMode.fixed_tip);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sofia", 0m);
          Share share2 = new Share("Kim", 30m);
          Share share3 = new Share("Jessica", 60m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 1, 1, true);
          Attendee attendee2 = new Attendee("Jessica", 2, 1, true);
          Attendee attendee3 = new Attendee("Sofia", 1, 1, false);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3);
          Share testShare = new Share(); 

          //Act
          decimal calculatedTip = bill.calculateTip(bill.getSubTotal(), TipMode.fixed_tip, bill.getTip());
          bill.setTip(calculatedTip);
          List<Share> calculatedShares = testShare.CalculateShares(bill, attendees, RoundingMode.up);

          //Assert
          Assert.AreEqual(expectedShares[0].getAmount(), calculatedShares[0].getAmount());
          Assert.AreEqual(expectedShares[1].getAmount(), calculatedShares[1].getAmount());
          Assert.AreEqual(expectedShares[2].getAmount(), calculatedShares[2].getAmount());
      }

      [TestMethod] //M4-20
      public void CalculateShares_TaxAndPercentTip_ApportionsTotal()
      {
          //Arrange
          Bill bill = new Bill(100, 13, 0.15m, TipMode.percent);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sofia", 33.34m);
          Share share2 = new Share("Kim", 33.34m);
          Share share3 = new Share("Jessica", 33.32m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 1, 1, true);
          Attendee attendee2 = new Attendee("Jessica", 1, 1, true);
          Attendee attendee3 = new Attendee("Michael", 1, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3);
          Share testShare = new Share(); 

          //Act
          decimal calculatedTip = bill.calculateTip(bill.getSubTotal(), TipMode.percent, bill.getTip());
          decimal grandTotal = bill.getSubTotal() + bill.getTax() + calculatedTip;
          List<Share> calculatedShares = testShare.CalculateShares(bill, attendees, RoundingMode.up);

          //Assert
          Assert.AreEqual(128.00m, grandTotal);
      }

      [TestMethod] //M4-21
      public void CalculateShares_TaxAndFixedTip_ApportionsTotal()
      {
          //Arrange
          Bill bill = new Bill(100, 13m, 20m, TipMode.percent);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sofia", 33.34m);
          Share share2 = new Share("Kim", 33.34m);
          Share share3 = new Share("Jessica", 33.32m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 1, 1, true);
          Attendee attendee2 = new Attendee("Jessica", 1, 1, true);
          Attendee attendee3 = new Attendee("Michael", 1, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3);
          Share testShare = new Share(); 

          //Act
          decimal calculatedTip = bill.calculateTip(bill.getSubTotal(), TipMode.fixed_tip, bill.getTip());
          decimal grandTotal = bill.getSubTotal() + bill.getTax() + calculatedTip;
          List<Share> calculatedShares = testShare.CalculateShares(bill, attendees, RoundingMode.up);

          //Assert
          Assert.AreEqual(133.00m, grandTotal);
      }

      [TestMethod] //M4-22
      public void CalculateShares_AllAttendeesExcluded_ThrowsException()
      {
          //Arrange
          Bill bill = new Bill(100, 4m, 0.20m, TipMode.percent);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sofia", 33.34m);
          Share share2 = new Share("Kim", 33.34m);
          Share share3 = new Share("Jessica", 33.32m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 1, 1, false);
          Attendee attendee2 = new Attendee("Jessica", 1, 1, false);
          Attendee attendee3 = new Attendee("Michael", 1, 1, false);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3);
          Share testShare = new Share();

          //Act and Assert
          Assert.Throws<ArgumentNullException>(() => testShare.CalculateShares(bill, attendees, RoundingMode.up));
      }

      [TestMethod] //M4-23
      public void CalculateShares_TotalWeightsZero_ThrowsException()
      {
          //Arrange
          Bill bill = new Bill(100, 4m, 0.20m, TipMode.percent);
          List<Share> expectedShares = new List<Share>();
          Share share1 = new Share("Sofia", 33.34m);
          Share share2 = new Share("Kim", 33.34m);
          Share share3 = new Share("Jessica", 33.32m);
          expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); 
          List<Attendee> attendees = new List<Attendee>();
          Attendee attendee1 = new Attendee("Kim", 0, 1, true);
          Attendee attendee2 = new Attendee("Jessica", 0, 1, true);
          Attendee attendee3 = new Attendee("Michael", 0, 1, true);
          attendees.Add(attendee1); attendees.Add(attendee2); attendees.Add(attendee3);
          Share testShare = new Share();

          //Act and Assert
          Assert.Throws<ArgumentNullException>(() => testShare.CalculateShares(bill, attendees, RoundingMode.up));
      }
    }
}









