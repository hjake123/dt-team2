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
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }
 //   public string Search { get; set; }
}