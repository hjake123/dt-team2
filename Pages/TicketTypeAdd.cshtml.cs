using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class TicketTypeAddModel : PageModel
{
    private readonly ILogger<TicketTypeAddModel> _logger;
    public TicketTypeAddModel(ILogger<TicketTypeAddModel> logger)
    {
        _logger = logger;
    }

    public int ticketTypeID = default!;
    public string ticketTypeLabel = default!;

    public void OnPost(LookUp_TicketType ticketAdd) {
        ticketTypeID = ticketAdd.ticketTypeID;
        ticketTypeLabel = ticketAdd.ticketTypeLabel;

        //connect to database
        if(ticketTypeID != 0)
        {
            string connectionString = CSHolder.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_TicketType(TicketType, TicketTypeLabel) VALUES ( @ticketTypeID, @ticketTypeLabel)", conn);
                selectCommand.Parameters.Add(new SqlParameter("ticketTypeID", ticketTypeID));
                selectCommand.Parameters.Add(new SqlParameter("ticketTypeLabel", ticketTypeLabel));          
                selectCommand.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Ticket Type Added");
            Response.Redirect("SellATicket");
        }
        else{
            Console.WriteLine("Invalid Ticket Type ID:" + ticketTypeID);
        }
    }
    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }
}