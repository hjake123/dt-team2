using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class AccessTypeAddModel : PageModel
{
    private readonly ILogger<AccessTypeAddModel> _logger;
    public AccessTypeAddModel(ILogger<AccessTypeAddModel> logger)
    {
        _logger = logger;
    }

    public int accessTypeID = default!;
    public string accessTypeLabel = default!;
    public ActionResult AccessTypeAdd_DE(LookUp_AccessType accessAdd){
        accessTypeID = accessAdd.accessTypeID;
        accessTypeLabel = accessAdd.accessTypeLabel;

        return null;
    }

    public void OnPost(LookUp_AccessType accessAdd) {
        accessTypeID = accessAdd.accessTypeID;
        accessTypeLabel = accessAdd.accessTypeLabel;
        Console.WriteLine(accessTypeID);
        Console.WriteLine(accessTypeLabel);

        
        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_AccessType(AccessType, AccessTypeLabel) VALUES ('" + accessTypeID + "', '" + accessTypeLabel + "')", conn);      
            selectCommand.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine("Access Type Added");
    }
      
}