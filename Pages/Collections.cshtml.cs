using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class CollectionsOutput
{
    //Transactions Enitity Attributes
    public string CollectionName { get; set; } = default!;
    public string Description { get; set; } = default!;
}

public class Collections
{
    //Transactions Enitity Attributes
    public string CollectionName { get; set; } = default!;
    public string Description { get; set; } = default!;
}

public class CollectionsModel : PageModel
{
    private readonly ILogger<CollectionsModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<CollectionsOutput> col_output = new List<CollectionsOutput>();

    public CollectionsModel(ILogger<CollectionsModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        GetCollections();
    }

    private void GetCollections()
    {
        //default query (nothing searched)
        if (col_output.Count > 0)
        {
            col_output.Clear();
        }
        //Connect to database
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                //            Console.WriteLine("Database open");
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Collections]", conn);
                SqlDataReader results = selectCommand.ExecuteReader();

                List<CollectionsOutput> temp_tr = new List<CollectionsOutput>();

                //Get Main Transaction entity Info
                while (results.Read())
                {
                    temp_tr.Add(new CollectionsOutput
                    {
                        CollectionName = results["CollectionName"].ToString(),
                        Description = results["Description"].ToString(),
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

                col_output = temp_tr;

                conn.Close();
            };
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string Search { get; set; } = default!;
}
