using System.Reflection.Metadata.Ecma335;

public class Attendee {

    //Decleration
    private String name = "";
    public int weight = 0; //for unequal cost shares
    private double share = 2;
    private bool included = true; //active in a split

    //Constructor to build an attendee object =-------------------------------------=
    public Attendee(String name, int weight, double share, bool included)
    {
        this.name = name;
        this.weight = weight;
        this.share = share;
        this.included = included;
    }

    //Setters and Getters =-----------------------------------------------------=    
    public void setName(String name) {this.name = name;}
    public void setWeight(int weight) {this.weight = weight;}
    public void setShare(double share) {this.share = share;}
    public void setIncluded(bool included) {this.included = included;}

    public String getName() {return name;}
    public int getWeight() {return weight;}
    public double getShare() {return share;}
    public bool getIncluded() {return included;}

}