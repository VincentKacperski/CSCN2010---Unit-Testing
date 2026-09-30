using System.Diagnostics.Tracing;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;

public class Bill
{

    private decimal subTotal;
    private decimal tax;
    private decimal tip_input;
    private TipMode tip_mode;

    //Constructors=---------------------------------------------------------=
    public Bill(decimal subTotal, decimal tax, decimal tip_input, TipMode tm) {
        this.subTotal = subTotal;
        this.tax = tax;
        this.tip_input = tip_input;
        this.tip_mode = tm;
    }

    //Setters and Getters=--------------------------------------------------=
    public void setSubTotal(decimal sub_total) {this.subTotal = sub_total;} //Set the total
    public void setTax(decimal tax) {this.tax = tax;} //Set the tip amount for fixed tip
    public void setTip(decimal tip_input) {this.tip_input = tip_input;} //Set the tip amount for fixed tip
    public void setTipMode(TipMode tip_mode) {this.tip_mode = tip_mode;} //Set the tip amount for fixed tip

    //=---------------------------------------------------------------------=
    public decimal getSubTotal() {return subTotal;} //get the bills total
    public decimal getTax() {return tax;} //get the bills tip  
    public decimal getTip() {return tip_input;} //get the bills total
    public TipMode getTipMode() {return tip_mode;} //get the bills tip        

    //CalculateTip Method
    public decimal calculateTip(decimal billTotal, TipMode tm, decimal tip_input)
    {

        //Decleration
        decimal calculated_tip = 0;

        //Calculate tip
        if (billTotal >= 0 && tip_input >= 0) { 

            switch (tm)
            {
                case TipMode.none: //no tip
                    calculated_tip = tip_input;
                    break;
                case TipMode.fixed_tip:  //fixed tip
                    calculated_tip = billTotal + tip_input - billTotal;
                    break;
                case TipMode.percent: //percentage tip
                    calculated_tip = billTotal * tip_input;
                    break;
                default:
                    //Nothing to do here
                    break;
            }
            return calculated_tip;

        } else
        {
            throw new ArgumentOutOfRangeException();   
        }
    }

    public decimal Validate(Bill bill, List<Attendee> attendees)
    {
        //validate Bill First
        if (bill.getSubTotal() < 0)
        {
            return -1; //invalidate the bill with attendees
        } else if (bill.getTax() < 0)
        {
            return -1; //invalidate the bill with attendees
        } else if (bill.getTip() < 0)
        {
            return -1; //Invalidate the bill with attendees
        }

        //Validate Attendee
        for (int i = 0; i < attendees.Count(); i++)
        {
            if (attendees[i].getName() == "")
            {
                return -1; //Invalidate the attendee list
            } else if (attendees[i].getWeight() <= 0)
            {
                return -1; //Invalidate the attendee list
            } else if (attendees[i].getShare() <= 0)
            {
                return -1; //Invalidate the attendee list
            }
        }
        return 0;
    }

}