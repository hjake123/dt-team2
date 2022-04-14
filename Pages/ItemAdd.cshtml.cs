using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class ItemAddModel : PageModel
{
    private readonly ILogger<ItemAddModel> _logger;
    public ItemAddModel(ILogger<ItemAddModel> logger)
    {
        _logger = logger;
    }

    public int itemID = default!;
    public string itemLabel = default!;

    public ActionResult ItemAdd_DE(Lookup_Item itemAdd){
        itemID = itemAdd.itemID;
        itemLabel = itemAdd.itemLabel;
        
        return null;
    }

    public void OnPost(Lookup_Item itemAdd) {
        itemID = itemAdd.itemID;
        itemLabel = itemAdd.itemLabel;

        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_Item(Item, ItemLabel) VALUES ('" + itemID + "', '" + itemLabel + "')", conn);      
            selectCommand.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine("Item Added");
    }
}