namespace OmaDW.Web2.Data;

public class MapPaymentForTime
{
    private decimal? _amount;
    private bool _fullPaymentMapped;
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public Transaction? Transaction { get; set; }

    public bool FullPaymentMapped
    {
        get => _fullPaymentMapped;
        set
        {
            if(value == true)
            {
                _amount = Transaction?.Amount ?? 0;
            }
            _fullPaymentMapped = value;
        }
    }

    public MapPaymentForTime()
    {

    }

    public MapPaymentForTime(TransactionContainer transaction)
    {
        FromDate = transaction.OriginalTransaction.Date;
        ToDate = transaction.OriginalTransaction.Date;
        Transaction = transaction.OriginalTransaction;
        Amount = transaction.OriginalTransaction.Amount;
        FullPaymentMapped = true;

    }

    public decimal Amount
    {
        get => _amount ?? Transaction.Amount;
        set => _amount = value;
    }
}
public class MapPaymentForCategory
{
    public Transaction? TransactionMapped { get; set; }
    public string? ReceiverMapped { get; set; }

    public string Category { get; set; }
    public string SubCategory { get; set; }
    public MapPaymentForCategory()
    {

    }

    public MapPaymentForCategory(TransactionContainer transaction)
    {
        TransactionMapped = transaction.OriginalTransaction;
    }
    public MapPaymentForCategory(string receiver)
    {
        ReceiverMapped = receiver;
    }
}