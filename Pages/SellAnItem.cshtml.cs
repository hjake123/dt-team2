using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class SellAnItemModel : PageModel
{
    private readonly ILogger<SellAnItemModel> _logger;
    public SellAnItemModel(ILogger<SellAnItemModel> logger)
    {
        _logger = logger;
        items = GetItems();
    }
    
    public int itemID{get; set;} = default!;
    public List<SelectListItem> items{get; set;} = new List<SelectListItem>();
    public float price{get; set;} = default!;
    public DateTime date{get; set;} = default!;

    public void OnPost(Transactions item) {
        itemID = item.itemID;
        price = item.price;
        date = item.date;          
        //connect to database and insert query

        if(itemID != 0){ 
            string connectionString = CSHolder.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Transactions(Item, Date, Price, IsTicket) VALUES ('" + itemID + "', '" + date + "' , '" + price + "', 'FALSE')", conn);      
                selectCommand.ExecuteNonQuery();
                conn.Close();
            }
            Console.WriteLine("Item Sold!");
        }
        else{
            Console.WriteLine("Invalid Item ID:" + itemID);
        }

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


