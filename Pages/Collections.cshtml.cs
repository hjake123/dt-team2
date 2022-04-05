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
    }

    public string Search { get; set; } = default!;
}
