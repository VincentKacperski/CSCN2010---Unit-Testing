using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RoundShare
{
    [TestClass]
    public class RoundShare
    {
        [TestMethod] //M2-06
        public void RoundShares_NoRounding_PreservesRawDecimals()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>();
            List<Share> rawShares = new List<Share>();
            Share share1 = new Share("Vincent", 33.752m);
            Share share2 = new Share("Jacob", 33.565m);
            rawShares.Add(share1);
            rawShares.Add(share2);
            expectedShares.Add(share1);
            expectedShares.Add(share2);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundMode.none);

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares);  
        }

        [TestMethod] //M2-07
        public void RoundShares_BankersRounding_RoundsToNearestEven()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>();
            List<Share> rawShares = new List<Share>();
            Share share1 = new Share("Vincent", 33.333m);
            Share share2 = new Share("Jacob", 33.333m);
            Share share3 = new Share("Mike", 33.333m);
            rawShares.Add(share1);
            rawShares.Add(share2);
            rawShares.Add(share3);
            expectedShares.Add(share1);
            expectedShares.Add(share2);
            expectedShares.Add(share3);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundMode.bankers);
            expectedShares[2].setAmount(33.339m);

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares); 
        }

        [TestMethod] //M2-08
        public void RoundShares_RoundUp_AppliesCeilingToCents() 
        {
            //Arrange
            List<Share> expectedShares = new List<Share>();
            List<Share> rawShares = new List<Share>();
            Share share1 = new Share("Vincent", 33.333m);
            Share share2 = new Share("Jacob", 33.333m);
            Share share3 = new Share("Mike", 33.333m);
            rawShares.Add(share1);
            rawShares.Add(share2);
            rawShares.Add(share3);
            expectedShares.Add(share1);
            expectedShares.Add(share2);
            expectedShares.Add(share3);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundMode.up);
            expectedShares[2].setAmount(33.319m);

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares);
        }

        [TestMethod] //M2-09
        public void RoundShares_RoundDown_AppliesFloorToCents()
        {
            //Arrange
            List<Share> expectedShares = new List<Share>();
            List<Share> rawShares = new List<Share>();
            Share share1 = new Share("Vincent", 33.333m);
            Share share2 = new Share("Jacob", 33.333m);
            Share share3 = new Share("Mike", 33.333m);
            rawShares.Add(share1);
            rawShares.Add(share2);
            rawShares.Add(share3);
            expectedShares.Add(share1);
            expectedShares.Add(share2);
            expectedShares.Add(share3);
            Share blankShare = new Share();

            //Act
            List<Share> actualShares = blankShare.RoundShares(rawShares, RoundMode.down);
            expectedShares[2].setAmount(33.339m);

            //Assert
            CollectionAssert.AreEqual(expectedShares, actualShares);  
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