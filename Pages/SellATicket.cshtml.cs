using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class SellATicketModel : PageModel
{
    private readonly ILogger<SellATicketModel> _logger;
    public SellATicketModel(ILogger<SellATicketModel> logger)
    {
        _logger = logger;
        access = GetAccess();
        ticket = GetTicket();
    } 
    //Access & Ticket Lists---------------------------------------------------
    public int selectedAccess{get; set;} = default!;
    public List<SelectListItem> access{get; set;} = new List<SelectListItem>();

    public int selectedTicket{get; set;} = default!;
    public List<SelectListItem> ticket{get; set;} = new List<SelectListItem>();
    //Other-----------------------------------------------------------------
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;

    //Functions------------------------------------------------------------
    public IActionResult Ticket_DE(TicketTransactions tick){
        selectedAccess = tick.selectedAccess;
        selectedTicket = tick.selecetedTicket;
        price = tick.price;
        expirationDate = tick.expirationDate;        
        return null;
    }
    public ActionResult OnPost(TicketTransactions tick){
        selectedAccess = tick.selectedAccess;
        selectedTicket = tick.selecetedTicket;
        price = tick.price;
        expirationDate = tick.expirationDate;

        Console.WriteLine(selectedAccess);
        Console.WriteLine(selectedTicket);
        Console.WriteLine(price);
        Console.WriteLine(expirationDate);
        
        return null;
    }
    private List<SelectListItem> GetAccess(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        tempAccess.Add(new SelectListItem{Value = "0", Text ="Select Access Type"});
        //connect to database



        return tempAccess;
    }
    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        tempTicket.Add(new SelectListItem{Value = "0", Text ="Select Ticket Type"});
        //connect to database


        return tempTicket;
    }
}


