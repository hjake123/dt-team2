using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class TicketTypeRemoveModel : PageModel
{
    private readonly ILogger<TicketTypeRemoveModel> _logger;
    public TicketTypeRemoveModel(ILogger<TicketTypeRemoveModel> logger)
    {
        _logger = logger;
        tickets = GetTicket();
    }
    public List<SelectListItem> tickets{get; set;} = new List<SelectListItem>();
    public int ticketTypeID = default!;

    public ActionResult TicketRemove_DE(LookUp_TicketType ticketTypeRemove){
        ticketTypeID  = ticketTypeRemove.ticketTypeID;

        return null;
    }
    public void OnPost(LookUp_TicketType ticketTypeRemove) {
        ticketTypeID = ticketTypeRemove.ticketTypeID;

        Console.WriteLine("Attemp to Remove Ticket Type ID: " + ticketTypeID);

        //remove query database
    }

    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        tempTicket.Add(new SelectListItem{Value = "0", Text ="Select Ticket Type"});
        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        
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