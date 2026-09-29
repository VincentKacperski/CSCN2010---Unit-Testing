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
    public List<Share> RoundShares(List<Share> rawShares, RoundMode mode)
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
            case RoundMode.none: //no tip
                //Do Nothing
                break;
            case RoundMode.up: //percentage tip
                
                break;
            case RoundMode.down: //percentage tip
                 
                break;
            case RoundMode.bankers:
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
}
