using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;
public class DataReportModel : PageModel
{
    private readonly ILogger<DataReportModel> _logger;
 
    public DataReportModel(ILogger<DataReportModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
 //   public string Search { get; set; }
}