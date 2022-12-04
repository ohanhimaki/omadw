using Newtonsoft.Json;

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

    [JsonIgnore]
    public string MappedLabel =>
        ReceiverMapped ?? TransactionMapped?.Date.ToString("dd.MM.yyyy") + "-" + TransactionMapped.Receiver;

    public MapPaymentForCategory()
    {

    }

    //override equals and hashcode

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        MapPaymentForCategory t = (MapPaymentForCategory)obj;
        if (Category != t.Category || SubCategory != t.SubCategory)
        {
            return false;
        }

        if (ReceiverMapped != null && t.ReceiverMapped != null)
        {
            return ReceiverMapped == t.ReceiverMapped;
        }
        if (TransactionMapped != null && t.TransactionMapped != null)
        {
            return TransactionMapped == t.TransactionMapped;
        }

        return false;
    }

    protected bool Equals(MapPaymentForCategory other)
    {
        return Equals(TransactionMapped, other.TransactionMapped) && ReceiverMapped == other.ReceiverMapped && Category == other.Category && SubCategory == other.SubCategory;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(TransactionMapped, ReceiverMapped, Category, SubCategory);
    }
}
