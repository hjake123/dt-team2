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

    private bool DoesIDExist(int T_ID){
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT TransactionID FROM dbo.Transactions WHERE TransactionID = " + T_ID, conn); 
            SqlDataReader results = selectCommand.ExecuteReader();  

            while(results.Read()){
                if(results["TransactionID"].ToString() == T_ID.ToString()){
                    return true; 
                }
            }            

            conn.Close();
        }
        return false;
    }
    private int GenerateID(){
        Random rnd = new Random();
        int T_ID = rnd.Next();

        Console.WriteLine("Generating ID.....");
        //if T_ID already exists rnd.next(); constantyl check
        while(DoesIDExist(T_ID) == true){
            Console.WriteLine("There exists one ID for " + T_ID);
            T_ID = rnd.Next();
            Console.WriteLine("Generating New ID....");                
        }

        Console.WriteLine("Success Generating ID " + T_ID);    
        return T_ID;
    }

    public void OnPost(Transactions item) {
        itemID = item.itemID;
        price = item.price;
        date = item.date;          
        date = item.date;
        Console.WriteLine(itemID);
        //connect to database and insert query

        if(itemID != 0){ 
            string connectionString = CSHolder.GetConnectionString();

            int tmp_transactionID = GenerateID();

            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();   
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Transactions(TransactionID, Item, Date, Price, IsTicket) VALUES ('" + tmp_transactionID + "', '" + itemID + "', '" + date + "' , '" + price + "', 'FALSE')", conn);      
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

