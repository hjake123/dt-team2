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
    public ActionResult AccessTypeAdd_DE(LookUp_TicketType ticketAdd){
        ticketTypeID = ticketAdd.ticketTypeID;
        ticketTypeLabel =  ticketAdd.ticketTypeLabel;

        return null;
    }

    public void OnPost(LookUp_TicketType ticketAdd) {
        ticketTypeID = ticketAdd.ticketTypeID;
       ticketTypeLabel = ticketAdd.ticketTypeLabel;
        Console.WriteLine(ticketTypeID);
        Console.WriteLine(ticketTypeLabel);

        
        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_TicketType(TicketType, TicketTypeLabel) VALUES ('" + ticketTypeID + "', '" + ticketTypeLabel + "')", conn);      
            selectCommand.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine("Ticket Type Added");
        
    }
      
}