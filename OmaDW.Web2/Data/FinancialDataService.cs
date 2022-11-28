using Newtonsoft.Json;

namespace OmaDW.Web2;

public class FinancialDataService
{
    private readonly IConfiguration _configuration;
    private bool _initialized { get; set; } = false;

    public FinancialDataService(IConfiguration configuration)
    {
        _configuration = configuration;
        Console.WriteLine("Aloitetaan tiedostojen luku");
        Initialize();
    }

    //event RefreshDataEventHandler RefreshData;
    public event EventHandler RefreshData;

    public async Task Initialize()
    {
        if(_initialized)
        {
            return;
        }
        try
        {
            // get path from appsettings
            var path = _configuration["Data:Path"];

            // transactions in transactions directory
            var files = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories).ToList();

            var allTransactions = new List<Transaction>();
            Console.WriteLine(files.Count);

            foreach (var file in files)
            {
                var transactions = (await File.ReadAllLinesAsync(file))
                    .Select((l, i) => { return i == 0 ? new string[] { } : l.Split(';'); })
                    .Where(x => x.Length > 0)
                    .Select(t =>
                        {
                            // log every field
                            // Console.WriteLine(t[0]);
                            // Console.WriteLine(t[1]);
                            // Console.WriteLine(t[2]);
                            // Console.WriteLine(t[3]);
                            // Console.WriteLine(t[4]);



                            return new Transaction
                            {
                                //Parse "" out of the string
                                Date = DateTime.Parse(t[0].Replace("\"", "")),
                                Receiver = t[1].Replace("\"", ""),
                                Description = t[2].Replace("\"", ""),
                                Message = t[3].Replace("\"", ""),
                                Amount = decimal.Parse(t[4].Replace("\"", ""))
                            };
                        }
                    )
                    .ToList();

                var minDate = transactions.Min(x => x.Date);
                allTransactions = allTransactions.Where(x => x.Date < minDate).ToList();
                allTransactions.AddRange(transactions);
            }


            Transactions = allTransactions;

            // log transactions as json to console
            // Console.WriteLine(JsonConvert.SerializeObject(Transactions));

            Console.WriteLine(Transactions.Sum(x => x.Amount));

            RefreshData?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        _initialized = true;
    }

    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public string Toimiiko { get; set; } = "Toimii";

    public async Task<List<Transaction>> GetTransactionsForApi()
    {
        //wait if not initialized
        while (!_initialized)
        {
            await Task.Delay(100);
        }

        return Transactions;
    }

    public async Task<List<Transaction>> GetTransactions()
    {
        //wait if not initialized
        while (!_initialized)
        {
            await Task.Delay(100);
        }

        return Transactions;
    }
}

public class Transaction
{
    public DateTime Date { get; set; }
    public string Receiver { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }
    public decimal Amount { get; set; }
}