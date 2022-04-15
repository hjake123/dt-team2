using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class ItemEditModel : PageModel
{
    private readonly ILogger<ItemEditModel> _logger;
    public ItemEditModel(ILogger<ItemEditModel> logger)
    {
        _logger = logger;
        items = GetItems();
    }
    public static List<SelectListItem> items{get; set;} = new List<SelectListItem>();
    public int itemID = default!;
    public string newItemLabel = default!;

    public void OnPost(Lookup_Item ItemEdit) {
        itemID = ItemEdit.itemID;
        newItemLabel = ItemEdit.newItemLabel;

        if(itemID != 0){  // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
            Console.WriteLine("Attempt to Edit Item Label: " + itemID + " To " + newItemLabel);

            //edit query database
            string connectionString = CSHolder.GetConnectionString();
            
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Lookup_Item SET ItemLabel = '" + newItemLabel + "' WHERE Item = " + itemID, conn);
                SqlDataReader results = selectCommand.ExecuteReader();                
                conn.Close();
            }
            Console.WriteLine("Item Label Edited!");
            
        }
    }

    private List<SelectListItem> GetItems(){
        List<SelectListItem> tempItems = new List<SelectListItem>();

        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Lookup_Item]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempItems.Add(new SelectListItem{Value = results["Item"].ToString(), Text = results["ItemLabel"].ToString()});  
            }
            
            conn.Close();
        }
        return tempItems;        
    }
}