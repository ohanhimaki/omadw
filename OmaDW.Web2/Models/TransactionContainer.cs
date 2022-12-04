using OmaDW.Web2.Data;

namespace OmaDW.Web2;

public class TransactionContainer
{
    public TransactionContainer(Transaction transaction, MapPaymentForTime singleOrDefault,
        List<MapPaymentForCategory> mapPaymentForCategories)
    {
        OriginalTransaction = transaction;
        MapPaymentForTime = singleOrDefault;
        MapPaymentForCategory = mapPaymentForCategories
                                    .SingleOrDefault(x =>
                                        x.TransactionMapped is not null && x.TransactionMapped.Equals(transaction)) ??
                                mapPaymentForCategories.SingleOrDefault(x =>
                                    x.ReceiverMapped is not null && x.ReceiverMapped.Equals(transaction.Receiver));
    }

    public MapPaymentForTime? MapPaymentForTime { get; private set; }
    public MapPaymentForCategory? MapPaymentForCategory { get; private set; }
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