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
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_TicketType(TicketType, TicketTypeLabel) VALUES ('" + ticketTypeID + "', '" + ticketTypeLabel + "')", conn);      
                selectCommand.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Ticket Type Added");
        }
        else{
            Console.WriteLine("Invalid Ticket Type ID:" + ticketTypeID);
        }
    }
}