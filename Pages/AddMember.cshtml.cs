using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class AddMemberModel : PageModel
{
    private readonly ILogger<AddMemberModel> _logger;
 
    public AddMemberModel(ILogger<AddMemberModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
 //   public string Search { get; set; }
}