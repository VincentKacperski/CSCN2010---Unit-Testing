using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq;

public class Share
{

    //Decleration
    private String name = "";
    private decimal amount = 0;

    //Constructor to build a share object =-------------------------------------=
    public Share() {}
    public Share(String name, decimal amount)
    {
        this.name = name;
        this.amount = amount;
    }

    //Setters and Getters =-----------------------------------------------------=    
    public void setName(String name) {this.name = name;} //Set a name
    public void setAmount(decimal amount) {this.amount = amount;} //Set an ampunt
    public String getName() {return name;} //Get the persons name
    public decimal getAmount() {return amount;} //Get the amount

    //Important Functions=------------------------------------------------------= 
    public List<Share> RoundShares(List<Share> rawShares, RoundingMode mode)
    {

        //Decleration
        decimal total = 0m;
        decimal roundedTotal = 0;
        decimal roundedNumber = 0m;
        decimal origNum = 0m;
        decimal diffrence = 0;

        if (rawShares.Count == 0) //Check if the array is empty
        {
            return rawShares;
        }

        //Determine a rounding mode
        switch (mode)
        {
            case RoundingMode.none: //no tip
                //Do Nothing
                break;
            case RoundingMode.up: //percentage tip
                for (int i = 0; i < rawShares.Count(); i++)
                {
                    total += rawShares[i].amount;
                    origNum = rawShares[i].amount;
                    roundedNumber = Math.Ceiling(rawShares[i].amount * 100) / 100;
                    rawShares[i].amount = roundedNumber;
                    roundedTotal += roundedNumber;
                    diffrence += roundedNumber - origNum;
                }
                rawShares[rawShares.Count()-1].amount -= roundedTotal - total;
                break;
            case RoundingMode.down: //percentage tip
                for (int i = 0; i < rawShares.Count(); i++)
                {
                    total += rawShares[i].amount;
                    origNum = rawShares[i].amount;
                    roundedNumber = Math.Floor(rawShares[i].amount * 100) / 100;
                    rawShares[i].amount = roundedNumber;
                    roundedTotal += roundedNumber;
                    diffrence += origNum - roundedNumber;
                }
                rawShares[rawShares.Count()-1].amount += diffrence;
                break;
            case RoundingMode.bankers:
                for (int i = 0; i < rawShares.Count(); i++)
                {
                total += rawShares[i].amount;
                roundedNumber = Math.Round(rawShares[i].amount, 2);
                rawShares[i].amount = roundedNumber;
                roundedTotal += roundedNumber;
                }
                rawShares[rawShares.Count()-1].amount += total - roundedTotal;
                break;
            default:
                //Nothing to do here
                break;
        }

        return rawShares;
    }

    public List<Share> CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode roundingMode)
    {
        //Decleration
        List<Share> calculatedShares = new List<Share>();
        decimal billTotal;
        decimal billShare;
        decimal roundedSum = 0;
        int attendeeCount;

        //Calculations
        billTotal = bill.getSubTotal() + bill.getTax() + bill.getTip();
        attendeeCount = attendees.Count();
        billShare = billTotal / attendeeCount;
        attendees.OrderByDescending(attendees => attendees.weight); //Sort the list from greatest to least ex. [8, 3, 2, 1]

        //Remove attendees who do not want to split the bill
        for (int i=0; i<attendees.Count(); i++)
        {
            //Check if the attendee agrees to split the bill
            if (attendees[i].getIncluded() == false)
            {
                attendeeCount -= 1; //if not subtract 1
                attendees.Remove(attendees[i]);
            }
        }

        //Sort the new array of attendees who are willing to share from greatest to least ex. [8, 3, 2, 1];
        attendees.OrderByDescending(attendees => attendees.weight);

        for (int j=0; j<attendeeCount; j++)
        {
            calculatedShares[j].amount = billShare * (decimal)attendees[j].getShare(); //Calculate their share.
            calculatedShares[j].name = attendees[j].getName(); //Track the attendees name with their share
        }

        //Determine a rounding mode
        switch (roundingMode)
        {
            case RoundingMode.none: //no tip
                //Do Nothing
                break;
            case RoundingMode.up: //round up in favour of the buissness
                for (int i = 0; i < calculatedShares.Count(); i++)
                {
                    decimal originalNum = calculatedShares[i].amount;
                    roundedSum += calculatedShares[i].amount; //Subtracted from the total to find the missing penny
                    decimal roundedNumber = Math.Ceiling(calculatedShares[i].amount * 100) / 100;
                    calculatedShares[i].amount = roundedNumber;
                }
                calculatedShares[calculatedShares.Count()-1].amount = roundedSum - billTotal;  
                break;
            case RoundingMode.down: //round down in favor of the customer
                for (int i = 0; i < calculatedShares.Count(); i++)
                {
                    decimal originalNum = calculatedShares[i].amount;
                    roundedSum += calculatedShares[i].amount;//Subtracted from the total to find the missing penny
                    decimal roundedNumber = Math.Floor(calculatedShares[i].amount * 100) / 100;
                    calculatedShares[i].amount = roundedNumber;
                }
                calculatedShares[calculatedShares.Count()-1].amount += billTotal - roundedSum; 
                break;
            case RoundingMode.bankers: //round in bankers mode to the nearest even
                for (int i = 0; i < calculatedShares.Count(); i++)
                {
                    decimal originalNum = calculatedShares[i].amount;
                    roundedSum += calculatedShares[i].amount;//Subtracted from the total to find the missing penny
                    decimal roundedNumber = Math.Round(calculatedShares[i].amount, 2);
                    calculatedShares[i].amount = roundedNumber;
                }
                calculatedShares[calculatedShares.Count()-1].amount += billTotal - roundedSum; 
                break;
            default:
                //Do nothing
                break;
        }
        return calculatedShares;
    }
}
