using Newtonsoft.Json;
using OmaDW.Web2.Data;

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
    private List<MapPaymentForTime> DataMappingsByTime { get; set; }
    public List<MapPaymentForCategory> DataMappingsByCategory { get; set; }

    public async Task Initialize()
    {
        //ReadDataMappingsFrom file and create a list of DataMappings



        if(_initialized)
        {
            return;
        }
        try
        {

            var dataMappings = await ReadDataMappingsByTimeFromFile();
            DataMappingsByTime = dataMappings;

            var dataMappingsByCategory = await ReadDataMappingsByCategoryFromFile();
            DataMappingsByCategory = dataMappingsByCategory;

            // get path from appsettings
            var path = _configuration["Data:Path"];

            // transactions in transactions directory
            var files = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories).ToList();

            var allTransactions = new List<Transaction>();

            foreach (var file in files)
            {
                var transactions = (await File.ReadAllLinesAsync(file))
                    .Select((l, i) => { return i == 0 ? new string[] { } : l.Split(';'); })
                    .Where(x => x.Length > 0)
                    .Select(t =>
                        {

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


    private async Task<List<MapPaymentForCategory>> ReadDataMappingsByCategoryFromFile()
    {
        //file is TransactionTimeMappings.json
        var path = _configuration["Data:SaveLocation"];
        // check if file exists
        // var file = Path.Combine(path, "TransactionTimeMappings.json");
        var file = Path.Combine(path, "TransactionCategoryMappings.json");
        if (!File.Exists(file))
        {
            // create file
            var dataMappingsTemp = new List<MapPaymentForCategory>();
            var jsonTemp = JsonConvert.SerializeObject(dataMappingsTemp);
            await File.WriteAllTextAsync(file, jsonTemp);
            return dataMappingsTemp;
        }
        else
        {
            var json = await File.ReadAllTextAsync(file);
            var dataMappings = JsonConvert.DeserializeObject<List<MapPaymentForCategory>>(json);
            return dataMappings;
        }
    }


    private async Task<List<MapPaymentForTime>> ReadDataMappingsByTimeFromFile()
    {
        //file is TransactionTimeMappings.json
        var path = _configuration["Data:SaveLocation"];
        // check if file exists
        // var file = Path.Combine(path, "TransactionTimeMappings.json");
        var file = Path.Combine(path, "TransactionTimeMappings.json");
        if (!File.Exists(file))
        {
            // create file
            var dataMappingsTemp = new List<MapPaymentForTime>();
            var jsonTemp = JsonConvert.SerializeObject(dataMappingsTemp);
            await File.WriteAllTextAsync(file, jsonTemp);
            return dataMappingsTemp;
        }
        else
        {
            var json = await File.ReadAllTextAsync(file);
            var dataMappings = JsonConvert.DeserializeObject<List<MapPaymentForTime>>(json);
            return dataMappings;
        }

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
    public async Task<List<TransactionContainer>> GetTransactionContainers()
    {
        var transactions = await GetTransactions();
        var transactionContainers = transactions.Select(x => new TransactionContainer(x,
            DataMappingsByTime.SingleOrDefault<MapPaymentForTime>(y =>
                y.Transaction.Equals(x)), DataMappingsByCategory )).ToList();

        return transactionContainers;

    }

    public async void AddMapping(MapPaymentForTime resultData)
    {
        DataMappingsByTime.Add(resultData);
        await SaveTransactionTimeMappingsFileAsync();

    }

    public async void RemoveMapping(MapPaymentForTime contextMapPaymentForTime)
    {
        DataMappingsByTime.Remove(contextMapPaymentForTime);
        await SaveTransactionTimeMappingsFileAsync();

    }

    private async Task SaveTransactionTimeMappingsFileAsync()
    {
        var path = _configuration["Data:SaveLocation"];
        var file = Path.Combine(path, "TransactionTimeMappings.json");
        var json = JsonConvert.SerializeObject(DataMappingsByTime);
        await File.WriteAllTextAsync(file, json);
    }
    private async Task SaveTransactionCategoryMappingsFileAsync()
    {
        var path = _configuration["Data:SaveLocation"];
        var file = Path.Combine(path, "TransactionCategoryMappings.json");
        var json = JsonConvert.SerializeObject(DataMappingsByTime);
        await File.WriteAllTextAsync(file, json);
    }
}