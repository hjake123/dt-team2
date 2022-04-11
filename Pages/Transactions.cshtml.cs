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

public class TicketType{
    public int id{get; set;} = default!;
    public string name{get; set;} = default!;
}

public class AccessType{
    public int id{get; set;} = default!;
    public string name{get; set;} = default!;
}

public class Transactions{
    //Transactions Enitity Attributes
    public string itemName{get; set;} = default!;
    public DateTime date{get; set;} = default!;
    public float price{get; set;} = default!;
    public bool IsTicket{get; set;} = default!;
    //TicketTransactions Entitiy attributes
    public DateTime expirationDate{get; set;} = default!;
    //convert to lists
    public List<AccessType> accessTypes{get; set;} = default!; 
    public List<TicketType> ticketType{get; set;} = default!;
}



