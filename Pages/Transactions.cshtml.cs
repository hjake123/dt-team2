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

    public string Search { get; set; }
}

public class Transactions{
    //Transactions Enitity Attributes
    public string itemName{get; set;} = default!;
    public DateTime date{get; set;} = default!;
    public float price{get; set;} = default!;
}

public class TicketTransactions{
    //TicketTransactions Entitiy attributes
    public string selectedAccess{get; set;} = default!; 
    public string selecetedTicket{get; set;} = default!;
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;
}



