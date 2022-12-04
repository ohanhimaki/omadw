namespace OmaDW.Web2;

public class Transaction
{
    public DateTime Date { get; set; }
    public string Receiver { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }
    public decimal Amount { get; set; }

    //override equals
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Transaction t = (Transaction)obj;
        return (Date == t.Date && Receiver == t.Receiver && Description == t.Description && Message == t.Message && Amount == t.Amount);
    }
}