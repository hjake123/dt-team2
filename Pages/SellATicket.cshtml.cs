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
    
    public bool IsTicket = true;
    public DateTime expirationDate = default!;
    public float price = default!;

    public List<AccessType> _access;
    public List<TicketType> _ticket;

    private static List<AccessType> PopulateAccess(){
        List<AccessType> tempAccess = new List<AccessType>();

        tempAccess.Add(new AccessType{
            id = 1, name ="General Admission"
            });

        return tempAccess;
    }

    public ActionResult Ticket_DE(Transactions ticket)
    {
        expirationDate = ticket.expirationDate;
        price = ticket.price;
        //copy access types & _ticket types from database?
        _access = PopulateAccess();
        
        return null;
    }

    public void OnPost(Transactions ticket) {
        expirationDate = ticket.expirationDate;
        price = ticket.price;
        //record access type and ticket type chosen
        Console.WriteLine("Ticket Expiration:");
        Console.WriteLine(expirationDate);
        Console.WriteLine("Ticket Price");
        Console.WriteLine(price);
    }
}


