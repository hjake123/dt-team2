using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class ReportOutput
{
    public string TransactionID = default!;
    public string Date = default!;
    public string Price = default!;
    public string ItemLabel = default!;
}

public class ReportInput
{
    public string DateFrom = default!;
    public string DateTo = default!;
    public string Item = default!;
}

public class DataReportModel : PageModel
{
    private readonly ILogger<DataReportModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<ReportOutput> col_output = new List<ReportOutput>();
    public DataReportModel(ILogger<DataReportModel> logger)
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

    public void OnPost(ReportInput c)
    {
        DateFrom = c.DateFrom;
        DateTo = c.DateTo;
        Item = c.Item;
        //connect to database and insert query
        if (TransactionID != "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand(
                    "SELECT a.TransactionID, a.Date, b.ItemLabel, a.Price " +
                    "FROM [dbo].[Transactions] AS a, [dbo].[Lookup_Item] AS b " +
                    //"WHERE a.Date >= Convert(datetime, '" + DateFrom + "') AND a.Date <= Convert(datetime, '" + DateTo + "') AND b.Item=2", conn);
                    "WHERE a.Date >= Convert(datetime, '2014-01-01') AND a.Date <= Convert(datetime, '2015-01-01') AND b.Item=2 ", conn);
                SqlDataReader results = selectCommand.ExecuteReader();
                List<ReportOutput> temp_tr = new List<ReportOutput>();
                while (results.Read())
                {
                    temp_tr.Add(new ReportOutput
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