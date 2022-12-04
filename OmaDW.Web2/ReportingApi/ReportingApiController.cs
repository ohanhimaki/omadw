using Microsoft.AspNetCore.Mvc;
using OmaDW.Web2;
using OmaDW.Web2.Data;

namespace OmaDW.Web2.ReportingApi;

[Route("api/[controller]")]
public class ReportingApiController
{
    private readonly FinancialDataService _financialDataService;
    private readonly AccountStatusService _accountStatusService;

    public ReportingApiController(FinancialDataService financialDataService,
        AccountStatusService accountStatusService)
    {
        _financialDataService = financialDataService;
        _accountStatusService = accountStatusService;
    }


    // GET: api/ReportingApi
    [HttpGet("[action]")]
    public async Task<List<Transaction>> GetOriginalTransactions()
    {
        var data = await _financialDataService.GetTransactionsForApi();

        //return data

        return data;
    }
    [HttpGet("[action]")]
    public async Task<List<AccountStatus>> GetAccountStatus()
    {
        var data = await _accountStatusService.GetAccountStatusesForApi();

        //return data

        return data;
    }
    [HttpGet("[action]")]
    public async Task<List<TransactionFlattened>> GetFinancialData()
    {
        var data = await _financialDataService.GetTransactionsWithMappingsFlattened();

        //return data

        return data.ToList();
    }
}