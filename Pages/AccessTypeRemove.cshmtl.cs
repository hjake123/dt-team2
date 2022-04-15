using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class AccessTypeRemoveModel : PageModel
{
    private readonly ILogger<AccessTypeRemoveModel> _logger;
    public AccessTypeRemoveModel(ILogger<AccessTypeRemoveModel> logger)
    {
        _logger = logger;
        access = GetAccess();
    }
    public static List<SelectListItem> access{get; set;} = new List<SelectListItem>();
    public int accessTypeID = default!;
    public void OnPost(LookUp_AccessType accessTypeRemove) {
        accessTypeID = accessTypeRemove.accessTypeID;
        Console.WriteLine("Attemp to Remove Access Type ID: " + accessTypeID);


        //remove query database
    }

    private List<SelectListItem> GetAccess(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();
        
        //connect to database
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_AccessType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempAccess.Add(new SelectListItem{Value = results["AccessType"].ToString(), 
                    Text = results["AccessTypeLabel"].ToString()});                
            }
            
            conn.Close();
        }
        return tempAccess;
    }
}