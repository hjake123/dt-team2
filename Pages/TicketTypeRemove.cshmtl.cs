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
    public static List<SelectListItem> tickets{get; set;} = new List<SelectListItem>();
    public int ticketTypeID = default!;

    public void OnPost(LookUp_TicketType ticketTypeRemove) {
        ticketTypeID = ticketTypeRemove.ticketTypeID;

        Console.WriteLine("Attempt to Remove Ticket Type ID: " + ticketTypeID);

        //remove query database
        
    }

    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_TicketType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempTicket.Add(new SelectListItem{Value = results["TicketType"].ToString(), 
                    Text = results["TicketTypeLabel"].ToString()});                
            }
            conn.Close();
        }
        
        return tempTicket;
    }
}