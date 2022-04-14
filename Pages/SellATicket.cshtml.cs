using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace dt_team2.Pages;


public class SellATicketModel : PageModel
{
    private readonly ILogger<SellATicketModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
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

        //connect insert into database
      
        return null;
    }
    private List<SelectListItem> GetAccess(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        tempAccess.Add(new SelectListItem{Value = "0", Text ="Select Access Type"});
        //connect to database

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_AccessType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempAccess.Add(new SelectListItem{Value = results["AccessType"].ToString(), 
                    Text = results["AccessTypeLabel"].ToString() + " // Access Type ID: " + results["AccessType"].ToString()});                
            }
            
            conn.Close();
        }
        return tempAccess;
    }
    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        tempTicket.Add(new SelectListItem{Value = "0", Text ="Select Ticket Type"});
        //connect to database

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_TicketType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempTicket.Add(new SelectListItem{Value = results["TicketType"].ToString(), 
                    Text = results["TicketTypeLabel"].ToString() + " // Ticket Type ID: " + results["TicketType"].ToString()});                
            }
            conn.Close();
        }
        
        return tempTicket;
    }
}


