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
    public string selectedAccess{get; set;} = default!;
    public List<SelectListItem> access{get; set;} = new List<SelectListItem>();

    public string selectedTicket{get; set;} = default!;
    public List<SelectListItem> ticket{get; set;} = new List<SelectListItem>();
    //Other-----------------------------------------------------------------
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;

    //Functions------------------------------------------------------------
    public IActionResult Ticket_DE(){
        
        return null;
    }
    public ActionResult OnPost(TicketTransactions tick){
        selectedAccess = tick.selectedAccess;
        selectedTicket = tick.selecetedTicket;
        price = tick.price;
        expirationDate = tick.expirationDate;

        return null;
    }
    private List<SelectListItem> GetAccess(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        //connect to database
        tempAccess.Add(new SelectListItem{Value = "1", Text ="General Admission"});
        tempAccess.Add(new SelectListItem{Value = "2", Text ="Special Exhibition"});

        return tempAccess;
    }
    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        //connect to database
        tempTicket.Add(new SelectListItem{Value = "1", Text ="Adult Ticket"});
        tempTicket.Add(new SelectListItem{Value = "2", Text ="Child Ticket"});

        return tempTicket;
    }
}


