using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class SellAnItemModel : PageModel
{
    private readonly ILogger<SellAnItemModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public SellAnItemModel(ILogger<SellAnItemModel> logger)
    {
        _logger = logger;
        items = GetItems();
    }

    public int itemID{get; set;} = default!;
    public List<SelectListItem> items{get; set;} = new List<SelectListItem>();
    public float price{get; set;} = default!;
    public DateTime date{get; set;} = default!;


    private List<SelectListItem> GetItems(){
        List<SelectListItem> tempItems = new List<SelectListItem>();

        tempItems.Add(new SelectListItem{Value = "0", Text ="Select Item Type"});
        //connect to database

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Lookup_Item]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                if(results["ItemLabel"].ToString() != "Ticket")
                {
                    tempItems.Add(new SelectListItem{Value = results["Item"].ToString(), Text = results["ItemLabel"].ToString()});  
                }
            }
            
            conn.Close();
        }
        return tempItems;
    }

    public ActionResult Item_DE(Transactions item){
        itemID = item.itemID;
        price = item.price;
        date = item.date;

        return null;
    }

    public void OnPost(Transactions item) {
        itemID = item.itemID;
        price = item.price;
        date = item.date;
        
        //connect to database and insert query
        try{
            /*
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO [dbo].[Transactions](Item, Date, Price, IsTicket) Value( " + itemID + ", " + date + ", " + price + ", False)", conn);      
                
                conn.Close();
            }*/
        }
        catch(Exception ex1){
            throw ex1;
        }
        
    }
}
