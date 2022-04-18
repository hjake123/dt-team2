using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class TransactionsEditModel : PageModel
{
    private readonly ILogger<TransactionsEditModel> _logger;
    public TransactionsEditModel(ILogger<TransactionsEditModel> logger)
    {
        _logger = logger;
        items = GetItems();
        access = GetAccess();
        ticket = GetTicket();
        GetTransactions();
    }
    public static List<TransactionsOutput> tr_output = new List<TransactionsOutput>();
    public List<SelectListItem> items{get; set;} = new List<SelectListItem>();
    public List<SelectListItem> access{get; set;} = new List<SelectListItem>();

    public List<SelectListItem> ticket{get; set;} = new List<SelectListItem>();

    public int transactionID{get; set;} = default!;
    //item label change variable
    public int itemID{get; set;} = default!;
    //date change variable 
    public DateTime date{get; set;} = default!;
    //price change variable
    public float price{get; set;} = default!;
    //expiration date change variable
    public DateTime expirationDate{get; set;} = default!;
    //access type change variable
    public int selectedAccess{get; set;} = default!;
    //ticket type change variable
    public int selectedTicket{get; set;} = default!;
    public void OnPost(Transactions tr, TicketTransactions tick) {
        transactionID = 0;//tr.transactionID;
        itemID = tr.itemID;
        date = tr.date;
        price = tr.price;
        expirationDate = tick.expirationDate;
        selectedAccess = tick.selectedAccess;
        selectedTicket = 0;//tick.selectedTicket;


        if(transactionID != 0){
            Console.WriteLine("Editing Transaction ID: " + transactionID + ".....");
            string connectionString = CSHolder.GetConnectionString();  
            if(itemID != 0){
                //connect to database 
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Transactions SET Item = " + itemID + " WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }

                Console.WriteLine("CHANGE ITEM ID: " + itemID);                
            }
            if(date != default!){
                //connect to database
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Transactions SET Date = '" + date + "' WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }
                Console.WriteLine("CHANGE Date: " + date);
            }
            if(price != default!){
                //connect to database
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Transactions SET Price = " + price + " WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }
                Console.WriteLine("CHANGE Price: " + price);
            }
            if(expirationDate != default!){
                //connect to database
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.TransactionsTicket SET ExpirationDate = '" + expirationDate + "' WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }
                Console.WriteLine("CHANGE ExpirationDate: " + expirationDate);
            }
            if(selectedAccess != 0){
                //connect to database
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.TransactionsTicket SET AccessType = " + selectedAccess + " WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }
                Console.WriteLine("CHANGE AccessType: " + selectedAccess);
            }
            if(selectedTicket != 0){
                //connect to database
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.TransactionsTicket SET TicketType = " + selectedTicket + " WHERE TransactionID = " + transactionID, conn);
                    selectCommand.ExecuteNonQuery();

                    conn.Close();
                }
                Console.WriteLine("CHANGE TicketType: " + selectedTicket);
            } 
        }
    }
    private List<SelectListItem> GetTicket(){
        List<SelectListItem> tempTicket = new List<SelectListItem>();

        tempTicket.Add(new SelectListItem{Value = "0", Text ="Select Ticket Type"});
        //connect to database
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_TicketType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempTicket.Add(new SelectListItem{Value = results["TicketType"].ToString(), 
                    Text = results["TicketTypeLabel"].ToString() + " // Ticket Type ID: " + results["TicketType"].ToString()});                
            }
            conn.Close();
        }
        
        return tempTicket;
    }
    private List<SelectListItem> GetAccess(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        tempAccess.Add(new SelectListItem{Value = "0", Text ="Select Access Type"});
        //connect to database
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_AccessType]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempAccess.Add(new SelectListItem{Value = results["AccessType"].ToString(), 
                    Text = results["AccessTypeLabel"].ToString() + " // Access Type ID: " + results["AccessType"].ToString()});                
            }
            
            conn.Close();
        }
        return tempAccess;
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
    private void GetTransactions()
    {
        //default query (nothing searched)
        if(tr_output.Count > 0){
            tr_output.Clear();
        }

        //Connect to database
        string connectionString = CSHolder.GetConnectionString();

        //NOTE: PROBABLY NOT THE BEST WAY TO DO THIS, USING 2 QUERIES. CONDENSE IF TIME PERMITS
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
//            Console.WriteLine("Database open");
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Transactions]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();

            List<TransactionsOutput> temp_tr = new List<TransactionsOutput>();

            //Get Main Transaction entity Info
            while(results.Read()){
                temp_tr.Add(new TransactionsOutput{TransactionID = results["TransactionID"].ToString()!,
                itemID = results["Item"].ToString()!,
                date = results["Date"].ToString()!,
                price = results["Price"].ToString()!,
                IsTicket = results["IsTicket"].ToString()!,
                TicketID = results["TicketID"].ToString()!});
            }

            //loop through transactions again to get all info needed
            for(int i = 0; i < temp_tr.Count; i++)
            {
                //Get Item Label
                int temp_itemID = Convert.ToInt32(temp_tr[i].itemID); 
                selectCommand = new SqlCommand("SELECT itemLabel FROM [dbo].[Lookup_Item] WHERE item = " + temp_itemID, conn);
                results = selectCommand.ExecuteReader();   
                while(results.Read()){
                    temp_tr[i].itemLabel = results["itemLabel"].ToString()!;              
                }

                //Get Ticket Transactions entity info
                if(Convert.ToBoolean(temp_tr[i].IsTicket) == false){
                    temp_tr[i].expirationDate = "NOT A TICKET";
                    temp_tr[i].TicketID = "NOT A TICKET";
                    temp_tr[i].accessType = "NOT A TICKET";
                    temp_tr[i].ticketType = "NOT A TICKET";                
                }
                else if(Convert.ToBoolean(temp_tr[i].IsTicket) == true){

                    //select from accesstable and tickettable
                    selectCommand = new SqlCommand("SELECT * FROM [dbo].[TransactionsTicket] " , conn);
                    results = selectCommand.ExecuteReader();   

                    while(results.Read()){
                        temp_tr[i].expirationDate = results["ExpirationDate"].ToString()!;                        
                        temp_tr[i].accessType = results["AccessType"].ToString()!;
                        temp_tr[i].ticketType = results["TicketType"].ToString()!;               
                    }                 
                }         
            }
    
            tr_output = temp_tr;

            conn.Close();
        };            
    }
}