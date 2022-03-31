using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class ExhibitionsModel : PageModel
{
    private readonly ILogger<ExhibitionsModel> _logger;

    public ExhibitionsModel(ILogger<ExhibitionsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public string Search { get; set; }
}
