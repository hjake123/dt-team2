using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class ShopReportOutput
{
    //Transactions Enitity Attributes
    public string TransactionID { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public string Item { get; set; } = default!;
    public int Price { get; set; } = default!;
}

public class ShopReportModel : PageModel
{
    private readonly ILogger<ShopReportModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<ShopReportOutput> shop_output = new List<ShopReportOutput>();

    public ShopReportModel(ILogger<ShopReportModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        GetShopReport();
    }

    private void GetShopReport()
    {
        //default query (nothing searched)
        if (shop_output.Count > 0)
        {
            shop_output.Clear();
        }
        //Connect to database
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //            Console.WriteLine("Database open");
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Transactions]", conn);
                SqlDataReader results = selectCommand.ExecuteReader();

                List<ShopReportOutput> temp_tr = new List<ShopReportOutput>();

                //Get Main Transaction entity Info
                while (results.Read())
                {
                    temp_tr.Add(new ShopReportOutput
                    {
                        TransactionID = results["TransactionID"].ToString(),
                        //Date = results["Date"].ToString(),
                        Item = results["Item"].ToString(),
                        //Price = results["Price"],
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

                shop_output = temp_tr;

                conn.Close();
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

