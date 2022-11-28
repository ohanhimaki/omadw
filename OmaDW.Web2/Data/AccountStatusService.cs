using Newtonsoft.Json;

namespace OmaDW.Web2.Data;

public class AccountStatusService
{
    private readonly FinancialDataService _financialDataService;
    private bool _initialized = false;

    public AccountStatusService(FinancialDataService financialDataService)
    {
        _financialDataService = financialDataService;
        Initialize();
    }

    private async void Initialize()
    {
        var datas = await _financialDataService.GetTransactions();
        var datasGroupedByDate = datas.GroupBy(x => x.Date.ToInt()).ToDictionary(x => x.Key, x => x.Sum(x => x.Amount));
        //console write datasgroupedbydate as json

        Console.WriteLine(JsonConvert.SerializeObject(datasGroupedByDate));

        var statusPrefixed = new {Date = new DateTime(2022,1,1), Amount = (decimal)25000}; //bogusdatafor development
        var allStatuses = new List<AccountStatus>();
        allStatuses.Add(new AccountStatus()
        {
            Date = statusPrefixed.Date,
            Amount = statusPrefixed.Amount
        });

        var date = statusPrefixed.Date.AddDays(-1);
        var minDate = datasGroupedByDate.Select(x => x.Key).Min(x => x);
        var currentStatus = statusPrefixed.Amount;
        while (date.ToInt() >= minDate)
        {
            if(datasGroupedByDate.TryGetValue(date.ToInt(), out var amount))
            {
                currentStatus -= amount;
            }
            allStatuses.Add(new AccountStatus()
            {
                Date = date,
                Amount = currentStatus
            });
            date = date.AddDays(-1);

        }
         date = statusPrefixed.Date.AddDays(1);
        var maxDate = datasGroupedByDate.Select(x => x.Key).Max(x => x);
         currentStatus = statusPrefixed.Amount;
        while (date.ToInt() <= maxDate)
        {
            if(datasGroupedByDate.TryGetValue(date.ToInt(), out var amount))
            {
                currentStatus += amount;
            }
            allStatuses.Add(new AccountStatus()
            {
                Date = date,
                Amount = currentStatus
            });
            date = date.AddDays(1);

        }

        AccountStatuses = allStatuses;





        _initialized = true;
    }

    public List<AccountStatus> AccountStatuses { get; set; }

    public async Task<List<AccountStatus>> GetAccountStatusesForApi()
    {
        //wait if not initialized
        while (!_initialized)
        {
            await Task.Delay(100);
        }

        return AccountStatuses;
    }
}

public class AccountStatus
{
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}