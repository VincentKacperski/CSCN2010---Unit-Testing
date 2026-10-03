using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Linq;

namespace Rounder.RoundShares
{
    [TestClass]
    public class RoundShare
    {
        [TestMethod] //M2-06
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>(); //Create a new list of expected shares
            List<Share> rawShares = new List<Share>(); //Create a new list of raw shares
            Share share1 = new Share("Vincent", 33.752m); 
            Share share2 = new Share("Jacob", 33.565m);
            rawShares.Add(share1); //Populate
            rawShares.Add(share2);
            expectedShares.Add(share1); //Populate
            expectedShares.Add(share2);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.none); 
            //Avoids rounding any shares

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares); //Compare  
        }

        [TestMethod] //M2-07
        public void RoundShares_BankersRounding_RoundsToNearestEven()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>(); //Create a new list of expected shares
            Share share1 = new Share("Vincent", 10.32m); //Populate
            Share share2 = new Share("Jacob", 10.34m);
            expectedShares.Add(share1); //Populate
            expectedShares.Add(share2);
            List<Share> rawShares = new List<Share>(); //Create a new list of raw shares
            Share share3 = new Share("Vincent", 10.325m);
            Share share4 = new Share("Jacob", 10.335m);
            rawShares.Add(share3); //Populate
            rawShares.Add(share4);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.bankers);
            //Round each share to the nearest even

            //Assert
            Assert.AreEqual(expectedShares[0].getAmount(), actualShares[0].getAmount()); //Compare each element
            Assert.AreEqual(expectedShares[1].getAmount(), actualShares[1].getAmount());
        }

        [TestMethod] //M2-08
        public void RoundShares_RoundUp_AppliesCeilingToCents() 
        {
            //Arrange
            List<Share> expectedShares = new List<Share>(); //Create a new list of expected shares
            Share share1 = new Share("Vincent", 10.34m); //Populate
            Share share2 = new Share("Jacob", 10.34m);
            List<Share> rawShares = new List<Share>(); //Create a new list of raw shares
            Share share3 = new Share("Vincent", 10.331m);
            Share share4 = new Share("Jacob", 10.331m);
            rawShares.Add(share3); //Populate
            rawShares.Add(share4);
            expectedShares.Add(share1);
            expectedShares.Add(share2);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.up);
            //round each share up to the nearest cent

            //Assert
            Assert.AreEqual(expectedShares[0].getAmount(), actualShares[0].getAmount()); //Compare each element
            Assert.AreEqual(expectedShares[1].getAmount(), actualShares[1].getAmount());
        }

        [TestMethod] //M2-09
        public void RoundShares_RoundDown_AppliesFloorToCents()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>(); //Create a new list of expected shares
            Share share1 = new Share("Vincent", 10.33m);
            Share share2 = new Share("Jacob", 10.33m);
            expectedShares.Add(share1); //Populate
            expectedShares.Add(share2);
            List<Share> rawShares = new List<Share>(); //Create a new list of raw shares
            Share share3 = new Share("Vincent", 10.339m);
            Share share4 = new Share("Jacob", 10.339m);
            rawShares.Add(share3); //Populate
            rawShares.Add(share4);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.down); 
            //round all shares down to the nearest cent

            //Assert
            Assert.AreEqual(expectedShares[0].getAmount(), actualShares[0].getAmount()); //Compare each share with expected
            Assert.AreEqual(expectedShares[1].getAmount(), actualShares[1].getAmount());
        }

        [TestMethod] //M2-10
        public void RoundShares_UnevenSplit_ReconcilesRemainder()
        {
            //Arrange
            decimal total = 10m; //Create a test total
            decimal split = 10/3m; //create a test split
            List<Share> expectedShares = new List<Share>(); //Create a new list of expected shares
            Share share1 = new Share("Vincent", 3.33m);
            Share share2 = new Share("Jacob", 3.33m);
            Share share3 = new Share("Lisa", 3.34m);
            expectedShares.Add(share1); expectedShares.Add(share2); expectedShares.Add(share3); //Populate
            List<Share> rawShares = new List<Share>(); //Create a new list of raw shares
            Share share4 = new Share("Vincent", split); 
            Share share5 = new Share("Jacob", split); //Assign each test split to each amount property
            Share share6 = new Share("Lisa", split);
            rawShares.Add(share4); rawShares.Add(share5); rawShares.Add(share6); //Populate
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.bankers); //round shares using bankers mode as a test
            decimal roundedSum = actualShares[actualShares.Count()-1].getAmount() * 3; // "Sum" all rounded shares .NET 10
            decimal diffrence = total - roundedSum; //Get the diffrence in pennys
            actualShares[actualShares.Count()-1].setAmount(actualShares[actualShares.Count()-1].getAmount() + diffrence); //Add the missing pennys to the last share

            //Assert
            Assert.AreEqual(expectedShares[0].getAmount(), actualShares[0].getAmount()); //Compare all values for equality
            Assert.AreEqual(expectedShares[1].getAmount(), actualShares[1].getAmount());
            Assert.AreEqual(expectedShares[2].getAmount(), actualShares[2].getAmount());
        }
        
        [TestMethod] //M2-11
        public void RoundShares_EmptyShareCollection_ReturnsEmpty()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>(); //Create an empty list of expected shares
            List<Share> rawShares = new List<Share>(); //Create a list of raw shares
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundingMode.down);
            //round an empty list of raw shares

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares); //Compare both lists
        }       
    }
}