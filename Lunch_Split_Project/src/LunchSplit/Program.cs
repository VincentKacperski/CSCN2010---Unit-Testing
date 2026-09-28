using System.Reflection.Metadata.Ecma335;

Console.WriteLine("Hello, World!");

//(Splitter)Exposes ComputeTip(decimal subtotal, TipMode mode, decimal tipInput) -> decimal tip 
//and CalculateShares(Bill bill, List<Attendee> attendees, RoundingMode roundingMode) -> List<Share>

//(Rounder)Exposes RoundShares(List<Share> rawShares, RoundingMode mode) -> List<Share>.
//Must implement the Penny Reconciliation algorithm

