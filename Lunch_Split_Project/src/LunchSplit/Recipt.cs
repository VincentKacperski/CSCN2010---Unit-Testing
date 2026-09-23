using System.Numerics;
using System.Security.Cryptography.X509Certificates;

class Recipt
{
    //Purchase data
    decimal total;
    decimal itemCount;
    decimal taxes;
    decimal tip;

    //Individual data
    String phone = "";
    String email = "";
    String fullName = "";

    //Setters and getters for purchase data
    public void setTotal(decimal total) //Set the total
    {
        this.total = total;
    }

    public void setItemCount(decimal itemCount) //Set the item count
    {
        this.itemCount = itemCount;
    }

    public void setTaxes(decimal taxes) //Set the total tax
    {
        this.taxes = taxes;
    }

    public void setTip(decimal tip) //Set the tip
    {
        this.tip = tip;
    }

    public decimal getTotal(decimal total) //Set the total
    {
        return this.total;
    }

    public decimal getItemCount(decimal itemCount) //Set the item count
    {
        return this.itemCount;
    }

    public decimal getTaxes(decimal taxes) //Set the total tax
    {
        return this.taxes;
    }

    public decimal getTip(decimal tip) //Set the tip
    {
        return this.tip;
    }

    //Setters and getters for individual data

    public void setName(String fullName) //Set the persons full name
    {
        this.fullName = fullName;
    }

    public void setPhone(String phone) //Set the persons phone number
    {
        this.phone = phone;
    }

    public void setEmail(String email) //Set the persons email
    {
        this.email = email;
    }

    public String getName(String fullName) //Get the persons full name
    {
        return this.fullName;
    }

    public String getPhone(String phone) //Get the persons phone number
    {
        return this.phone;
    }

    public String getEmail(String email) //Get the persons email
    {
        return this.email;
    }

    //Important calculation methods
    public decimal formatRecipt()
    {
        return 0;
    }

}