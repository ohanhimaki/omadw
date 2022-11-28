namespace OmaDW.Web2;

public class Transaction
{
    public DateTime Date { get; set; }
    public string Receiver { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }
    public decimal Amount { get; set; }
}