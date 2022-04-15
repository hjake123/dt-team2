using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;
public class AddCollectionModel : PageModel
{
    private readonly ILogger<AddCollectionModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public AddCollectionModel(ILogger<AddCollectionModel> logger)
    {
        _logger = logger;
    }
    public string CollectionName = default!;
    public string Description = default!;

    /*public ActionResult Collection_DE(Collections c)
    {
        CollectionName = c.CollectionName;
        Description = c.Description;

        return null;
    }*/

    public void OnPost(Collections c)
    {
        CollectionName = c.CollectionName;
        Description = c.Description;

        //connect to database and insert query
        if (CollectionName != "")
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO [dbo].[Collections](CollectionName, Description) VALUES('" + CollectionName + "', '" + Description + "')", conn);
                selectCommand.ExecuteNonQuery();

                conn.Close();
            }
            Console.WriteLine("Collection Added");
        }
        else
        {
            //Console.WriteLine("Invalid Ticket Type ID:" );
        }

    }

}