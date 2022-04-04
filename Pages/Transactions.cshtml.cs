using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;

    public TransactionsModel(ILogger<TransactionsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }

    public string Search { get; set; }
}
