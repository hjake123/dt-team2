using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class ShopReportOutput
{
    public string TransactionID = default!;
    public string Date = default!;
    public string Price = default!;
    public string ItemLabel = default!;
}

public class ShopReportInput
{
    public string DateFrom = default!;
    public string DateTo = default!;
    public string Item = default!;
}

public class ShopReportModel : PageModel
{
    private readonly ILogger<ShopReportModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<ShopReportOutput> col_output = new List<ShopReportOutput>();
    public ShopReportModel(ILogger<ShopReportModel> logger)
    {
        _logger = logger;
    }

    public string TransactionID = default!;
    public string DateFrom = default!;
    public string DateTo = default!;
    public string Date = default!;
    public string Price = default!;
    public string Item = default!;
    public string ItemLabel = default!;

    public void OnPost(ShopReportInput c)
    {
        DateFrom = c.DateFrom;
        DateTo = c.DateTo;
        Item = c.Item;
        //connect to database and insert query
        if (TransactionID != "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //"WHERE a.Date >= Convert(datetime, '" + DateFrom + "') AND a.Date <= Convert(datetime, '" + DateTo + "') AND b.Item=2", conn);
                conn.Open();
                SqlCommand selectCommand = new SqlCommand(
                    "SELECT a.TransactionID, a.Date, b.ItemLabel, a.Price " +
                    "FROM [dbo].[Transactions] AS a, [dbo].[Lookup_Item] AS b " +
                    "WHERE a.Date >= Convert(datetime, '2015-01-01') AND a.Date <= Convert(datetime, '2015-12-12') AND b.Item=2", conn);
                SqlDataReader results = selectCommand.ExecuteReader();
                List<ShopReportOutput> temp_tr = new List<ShopReportOutput>();
                while (results.Read())
                {
                    temp_tr.Add(new ShopReportOutput
                    {
                        TransactionID = results["TransactionID"].ToString(),
                        Date = results["Date"].ToString(),
                        ItemLabel = results["ItemLabel"].ToString(),
                        Price = results["Price"].ToString(),
                    });
                }
                col_output = temp_tr;
                conn.Close();
            }
            
            Console.WriteLine("Report Generated");
        }
        else
        {
            //Console.WriteLine("Invalid Ticket Type ID:" );
        }

    }
}
