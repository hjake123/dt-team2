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

    public void OnPost(Lookup_Item itemAdd) {
        itemID = itemAdd.itemID;
        itemLabel = itemAdd.itemLabel;

        //connect to database
        if(itemID != 0){ // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
            string connectionString = CSHolder.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Lookup_Item(Item, ItemLabel) VALUES ( @itemID, @itemLabel)", conn);
                selectCommand.Parameters.Add(new SqlParameter("itemID", itemID));
                selectCommand.Parameters.Add(new SqlParameter("itemLabel", itemLabel));      
                selectCommand.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Item Added");
            Response.Redirect("SellAnItem");
        }
        else{
            Console.WriteLine("Invalid Item ID:" + itemID);
        }

    }
    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }
}