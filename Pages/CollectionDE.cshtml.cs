using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class CollectionDEModel : PageModel
{
    private readonly ILogger<CollectionDEModel> _logger;

    public CollectionDEModel(ILogger<CollectionDEModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    //   public string Search { get; set; }
}