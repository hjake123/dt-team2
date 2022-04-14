using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class ItemRemoveModel : PageModel
{
    private readonly ILogger<ItemRemoveModel> _logger;
    public ItemRemoveModel(ILogger<ItemRemoveModel> logger)
    {
        _logger = logger;
        items = GetItems();
    }
    public List<SelectListItem> items{get; set;} = new List<SelectListItem>();
    public int itemID = default!;

    public ActionResult ItemRemove_DE(Lookup_Item itemRemove){
        itemID = itemRemove.itemID;

        return null;
    }
    public void OnPost(Lookup_Item itemRemove) {
        itemID = itemRemove.itemID;

        Console.WriteLine("Attemp to Remove Item ID: " + itemID);

        //remove query database
    }

    private List<SelectListItem> GetItems(){
        List<SelectListItem> tempItems = new List<SelectListItem>();

        tempItems.Add(new SelectListItem{Value = "0", Text ="Select Item Type"});

        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Lookup_Item]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempItems.Add(new SelectListItem{Value = results["Item"].ToString(), Text = results["ItemLabel"].ToString() + "// Item ID: " + results["Item"].ToString()});  
            }
            
            conn.Close();
        }
        return tempItems;        
    }
}