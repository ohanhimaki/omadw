using Microsoft.AspNetCore.Mvc;
using OmaDW.Web2;

namespace OmaDW.Web2.ReportingApi;

[Route("api/[controller]")]
public class ReportingApiController
{
    private readonly FinancialDataService _financialDataService;
    public ReportingApiController(FinancialDataService financialDataService)
    {
        _financialDataService = financialDataService;
    }


    // GET: api/ReportingApi
    [HttpGet("[action]")]
    public async Task<List<Transaction>> GetFinancialData()
    {
        var data = await _financialDataService.GetTransactionsForApi();

        //return data

        return data;
    }
}