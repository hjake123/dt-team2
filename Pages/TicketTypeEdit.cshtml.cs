using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class TicketTypeEditModel : PageModel
{
    private readonly ILogger<TicketTypeEditModel> _logger;
    public TicketTypeEditModel(ILogger<TicketTypeEditModel> logger)
    {
        _logger = logger;
        tickets = GetTicket();
    }
    public static List<SelectListItem> tickets{get; set;} = new List<SelectListItem>();

    public int ticketTypeID = default!;
    public string ticketTypeLabel = default!;

    public void OnPost(LookUp_TicketType TicketTypeEdit) {
        ticketTypeID = TicketTypeEdit.ticketTypeID;
        ticketTypeLabel = TicketTypeEdit.ticketTypeLabel;
        
        if(ticketTypeID != 0){  // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
            Console.WriteLine("Attemp to Edit Ticket Label: " + ticketTypeID + " To " + ticketTypeLabel);
            //edit query database
            string connectionString = CSHolder.GetConnectionString();
            
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Lookup_TicketType SET TicketTypeLabel = @ticketTypeLabel WHERE TicketType = @ticketTypeID", conn);
                selectCommand.Parameters.Add(new SqlParameter("ticketTypeLabel", ticketTypeLabel));
                selectCommand.Parameters.Add(new SqlParameter("ticketTypeID", ticketTypeID));
                SqlDataReader results = selectCommand.ExecuteReader();                
                conn.Close();
            }
            Console.WriteLine("Ticket Type Label Edited!");
            Response.Redirect("SellATicket");
        }
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