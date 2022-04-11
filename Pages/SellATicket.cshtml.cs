using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class SellATicketModel : PageModel
{
    private readonly ILogger<SellATicketModel> _logger;
 
    public SellATicketModel(ILogger<SellATicketModel> logger)
    {
        _logger = logger;
    } 

}

