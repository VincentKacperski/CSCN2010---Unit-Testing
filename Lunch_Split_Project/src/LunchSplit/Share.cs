using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Linq;
using System.ComponentModel;

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
        List<Share> calculatedShares = new List<Share>(); //List to hold newly calculated shares
        decimal subTotal;
        decimal billShare;
        decimal roundedSum = 0;
        int sumWeights = 0;
        int attendeeCount;

        //Calculations
        subTotal = bill.getSubTotal(); //Get the subTotal for share calculations
        attendeeCount = attendees.Count(); //Get the total number of attendees

        //Exclude employees 
        for (int i = 0; i < attendeeCount; i++) //Loop through the attendee list
        {
            Console.WriteLine("Entering here.");
            if (attendees[i].getIncluded() == true) //Check if the attendee is included
            {          
                sumWeights += attendees[i].weight; //Sum the weights to divide the bill by
            } else {
                sumWeights += 0; //Don't add to the total weight if the attendee is excluded         
            }          
        }
        billShare = subTotal / sumWeights; //Divide the bill based on weights

        //Calculate shares for each attendee based on weight
        for (int i = 0; i < attendees.Count(); i++)
        {
            if (attendees[i].getIncluded() == true) //Check if the attendee is included in the share
            {
                calculatedShares.Add(new Share () //Create a new share object based on the attendee
                {
                    name = attendees[i].getName(),
                    amount = billShare * (decimal)attendees[i].getWeight(),
                });
            } else
            {
                calculatedShares.Add(new Share () //Add a 0 share amount for the excluded attendee
                {
                    name = attendees[i].getName(),
                    amount = 0,
                });
            }        
        } //Sort the list from least to greatest so we can add the penny to the largest share
        calculatedShares = calculatedShares.OrderBy(calculatedShares => calculatedShares.amount).ToList();

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
                    decimal roundedNumber = Math.Ceiling(calculatedShares[i].amount * 100) / 100;
                    calculatedShares[i].amount = roundedNumber;
                    roundedSum += calculatedShares[i].amount; //Subtracted from the total to find the missing penny
                }
                calculatedShares[calculatedShares.Count()-1].amount -= roundedSum - subTotal;
                break;
            case RoundingMode.down: //round down in favor of the customer
                /*for (int i = 0; i < calculatedShares.Count(); i++)
                {
                    decimal originalNum = calculatedShares[i].amount;
                    roundedSum += calculatedShares[i].amount;//Subtracted from the total to find the missing penny
                    decimal roundedNumber = Math.Floor(calculatedShares[i].amount * 100) / 100;
                    calculatedShares[i].amount = roundedNumber;
                }
                calculatedShares[calculatedShares.Count()-1].amount += subTotal - roundedSum; */
                break;
            case RoundingMode.bankers: //round in bankers mode to the nearest even
                /*for (int i = 0; i < calculatedShares.Count(); i++)
                {
                    decimal originalNum = calculatedShares[i].amount;
                    roundedSum += calculatedShares[i].amount;//Subtracted from the total to find the missing penny
                    decimal roundedNumber = Math.Round(calculatedShares[i].amount, 2);
                    calculatedShares[i].amount = roundedNumber;
                }
                calculatedShares[calculatedShares.Count()-1].amount += billTotal - roundedSum; */
                break;
            default:
                //Do nothing
                break;
        }
        return calculatedShares;
    }
}
