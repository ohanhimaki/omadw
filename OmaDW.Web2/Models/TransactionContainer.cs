using OmaDW.Web2.Data;

namespace OmaDW.Web2;

public class TransactionContainer
{
    public TransactionContainer(Transaction transaction, MapPaymentForTime singleOrDefault)
    {
        OriginalTransaction = transaction;
        MapPaymentForTime = singleOrDefault;
    }

    public MapPaymentForTime? MapPaymentForTime { get; private set; }
    public Transaction OriginalTransaction { get; private set; }

    public void SetMapPaymentForTime(MapPaymentForTime resultData)
    {
        MapPaymentForTime = resultData;
    }

    public void RemoveMapPaymentForTime()
    {
        MapPaymentForTime = null;
    }
}