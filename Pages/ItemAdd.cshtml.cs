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
        }
        else{
            Console.WriteLine("Invalid Item ID:" + itemID);
        }

    }
}