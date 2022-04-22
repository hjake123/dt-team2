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

    public void OnPost(LookUp_AccessType accessAdd) {
        accessTypeID = accessAdd.accessTypeID;
        accessTypeLabel = accessAdd.accessTypeLabel;
        Console.WriteLine(accessTypeID);
        Console.WriteLine(accessTypeLabel);

        //connect to database
        if(accessTypeID != 0) // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
        {
            string connectionString = CSHolder.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_AccessType(AccessType, AccessTypeLabel) VALUES (@accessTypeID, @accessTypeLabel)", conn);
                selectCommand.Parameters.Add(new SqlParameter("accessTypeID", accessTypeID));
                selectCommand.Parameters.Add(new SqlParameter("accessTypeLabel", accessTypeLabel));      
                selectCommand.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Access Type Added");
        }
        else{
            Console.WriteLine("Invalid Access Type ID:" + accessTypeID);
        }

    }
      
}