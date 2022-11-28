using Microsoft.AspNetCore.Mvc;

namespace OmaDW.Web.ReportingApi;

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
        var data = await _financialDataService.GetTransactions();

        //return data

        return data;
    }
}