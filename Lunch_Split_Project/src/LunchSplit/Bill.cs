using System.Numerics;
using System.Security.Cryptography.X509Certificates;

class Bill
{
    decimal total;
    decimal tip;
    decimal fixedValue;

    //Setters and Getters
    public void setTotal(decimal total) //Set the total
    {
        this.total = total;
    }

    public void setTip(decimal tip) //Set the total
    {
        this.tip = tip;
    }

    public void setFixedValue(decimal fixedValue) //Set the total
    {
        this.fixedValue = total;
    }

    //Important calculation methods
    public decimal calculateTip(decimal billTotal, decimal tm, decimal value)
    {
        return 0;
    }

}