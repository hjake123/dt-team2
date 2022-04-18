using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class CollectionsModel : PageModel
{
    private readonly ILogger<CollectionsModel> _logger;

    public CollectionsModel(ILogger<CollectionsModel> logger)
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

    public string Search { get; set; } = default!;
}
