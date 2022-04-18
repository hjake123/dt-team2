using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class TicketReportOutput
{
    //Ticket Enitity Attributes
    public int TicketID { get; set; } = default!;
    public DateTime ExpDate { get; set; } = default!;
    public int AccessType { get; set; } = default!;
    public int TicketType { get; set; } = default!;
    public int TransID { get; set; } = default!;
}

public class TicketReportModel : PageModel
{
    private readonly ILogger<TicketReportOutput> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<TicketReportOutput> ticket_output = new List<TicketReportOutput>();

    public TicketReportModel(ILogger<TicketReportOutput> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        GetTicketReport();
    }

    private void GetTicketReport()
    {
        //default query (nothing searched)
        if (ticket_output.Count > 0)
        {
            ticket_output.Clear();
        }
        //Connect to database
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //            Console.WriteLine("Database open");
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[TransactionsTicket]", conn);
                SqlDataReader results = selectCommand.ExecuteReader();

                List<TicketReportOutput> temp_tr = new List<TicketReportOutput>();

                //Get Main Transaction entity Info
                while (results.Read())
                {
                    temp_tr.Add(new TicketReportOutput
                    {
                        TicketID = int.Parse(results["TicketID"].ToString()),
                        AccessType = int.Parse(results["AccessType"].ToString()),
                        TicketType = int.Parse(results["TicketType"].ToString()),
                        TransID = int.Parse(results["TransactionID"].ToString())
                    });
                }

                //loop through transactions again to get all info needed
                /*for (int i = 0; i < temp_tr.Count; i++)
                {
                    //Get Item Label
                    int temp_colName = Convert.ToInt32(temp_tr[i].CollectionName);
                    selectCommand = new SqlCommand("SELECT itemLabel FROM [dbo].[Lookup_Item] WHERE item = " + temp_colName, conn);
                    results = selectCommand.ExecuteReader();
                    while (results.Read())
                    {
                        temp_tr[i].CollectionName = results["CollectionName"].ToString();
                    }
                }*/

                ticket_output = temp_tr;

                conn.Close();
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine("No, that's wrong!");
            throw ex;
        }
    }
}

