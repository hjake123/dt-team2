using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class SellAnItemModel : PageModel
{
    private readonly ILogger<SellAnItemModel> _logger;
 
    public SellAnItemModel(ILogger<SellAnItemModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

 //   public string Search { get; set; }
}