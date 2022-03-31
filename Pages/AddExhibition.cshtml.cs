using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class AddExhibitionModel : PageModel
{
    private readonly ILogger<AddExhibitionModel> _logger;

    public AddExhibitionModel(ILogger<AddExhibitionModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public string Search { get; set; }
}
