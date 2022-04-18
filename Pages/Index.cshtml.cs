using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public string username = default!;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }else{
            username = Request.Cookies["session_user"]!;
        }
    }
}
