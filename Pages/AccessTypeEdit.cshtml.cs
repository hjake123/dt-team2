using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class AccessTypeEditModel : PageModel
{
    private readonly ILogger<AccessTypeEditModel> _logger;
    public AccessTypeEditModel(ILogger<AccessTypeEditModel> logger)
    {
        _logger = logger;
        access = GetAccess();
    }
    public static List<SelectListItem> access{get; set;} = new List<SelectListItem>();

    public int accessTypeID = default!;
    public string accessTypeLabel = default!;

    public void OnPost(LookUp_AccessType accessTypeEdit) {
        accessTypeID = accessTypeEdit.accessTypeID;
        accessTypeLabel = accessTypeEdit.accessTypeLabel;
        Console.WriteLine(accessTypeID);
        
        if(accessTypeID != 0){  // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
            Console.WriteLine("Attemp to Edit Item Label: " + accessTypeID + " To " + accessTypeLabel);
            //edit query database
            string connectionString = CSHolder.GetConnectionString();
            
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Lookup_AccessType SET AccessTypeLabel = @accessTypeLabel WHERE AccessType = accessTypeID", conn);
                selectCommand.Parameters.Add(new SqlParameter("accessTypeLabel", accessTypeLabel));
                selectCommand.Parameters.Add(new SqlParameter("accessTypeID", accessTypeID));
                SqlDataReader results = selectCommand.ExecuteReader();                
                conn.Close();
            }
            Console.WriteLine("Access Type Label Edited!");
            Response.Redirect("SellATicket");
        }
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