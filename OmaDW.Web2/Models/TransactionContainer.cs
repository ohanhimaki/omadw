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

    public void SetMapPaymentForCategory(MapPaymentForCategory mapPaymentForCategory)
    {
        MapPaymentForCategory = mapPaymentForCategory;
    }
}

public static class TransactionContainerHelpers
{
    public static IEnumerable<TransactionFlattened> Flatten(this TransactionContainer transactionContainer)
    {
        List<DateTime> dates;
        if (transactionContainer.MapPaymentForTime is not null
            && transactionContainer.MapPaymentForTime.FromDate is not null
            && transactionContainer.MapPaymentForTime.ToDate is not null)
        {
            dates = transactionContainer.MapPaymentForTime.FromDate.Value.DatesTo(transactionContainer
                .MapPaymentForTime.ToDate.Value);
        }
        else
        {
            dates = new List<DateTime>() { transactionContainer.OriginalTransaction.Date };
        }
        foreach (var dateTime in dates)
        {
            //TODO calculate not fullpayment rows
            yield return new TransactionFlattened
            (
                Amount : transactionContainer.MapPaymentForTime is null ? transactionContainer.OriginalTransaction.Amount :  transactionContainer.MapPaymentForTime.Amount / dates.Count,
                Date : dateTime,
                Receiver : transactionContainer.OriginalTransaction.Receiver,
                Description : transactionContainer.OriginalTransaction.Description,
                Message : transactionContainer.OriginalTransaction.Message,
                Category : transactionContainer.MapPaymentForCategory?.Category,
                SubCategory : transactionContainer.MapPaymentForCategory?.SubCategory);



        }
    }
}

public record TransactionFlattened(
    DateTime Date,
    string Receiver,
    string Description,
    string Message,
    decimal Amount,
    string? Category,
    string? SubCategory);