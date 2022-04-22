using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class TransactionsOutput{
    //Transactions Enitity Attributes
    public string TransactionID{get; set;} = default!;
    public string itemID{get; set;} = default!;
    public string date{get; set;} = default!;
    public string price{get; set;} = default!;
    public string IsTicket{get; set;} = default!;
    public string TicketID{get; set;} = default!;
    //Item LookUp Table
    public string itemLabel{get; set;} = default!;
    //TransactionsTicket Table
    public string expirationDate{get; set;} = default!;
    public string accessType{get; set;} = default!;
    public string ticketType{get; set;} = default!;
    //AccessType Lookup

    //TicketType Lookup
}

public class Transactions{
    //Transactions Enitity Attributes
    public int itemID{get; set;} = default!;
    public DateTime date{get; set;} = default!;
    public float price{get; set;} = default!;
    public bool IsTicket{get; set;} = default!;
    public bool TicketID{get; set;} = default!;
    public int transactionID{get; set;} = default!;
}

public class TicketTransactions{
    //TicketTransactions Entitiy attributes
    public int selectedAccess{get; set;} = default!; 
    public int selectedTicket{get; set;} = default!;
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;
    
}


public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;

    public static List<TransactionsOutput> tr_output = new List<TransactionsOutput>();

    public TransactionsModel(ILogger<TransactionsModel> logger)
    {
        _logger = logger;

    }

    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
        GetTransactions();
    }

    private void GetTransactions()
    {
        //default query (nothing searched)
        if(tr_output.Count > 0){
            tr_output.Clear();
        }

        //Connect to database
        string connectionString = CSHolder.GetConnectionString();

        //NOTE: PROBABLY NOT THE BEST WAY TO DO THIS, USING 2 QUERIES. PROBABLY COULD CONDENSE
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
                    temp_tr[i].expirationDate = "-------------------";
                    temp_tr[i].TicketID = "NOT A TICKET";
                    temp_tr[i].accessType = "-------------------";
                    temp_tr[i].ticketType = "-------------------";                
                }
                else if(Convert.ToBoolean(temp_tr[i].IsTicket) == true){

                    //select from accesstable and tickettable
                    selectCommand = new SqlCommand("SELECT ExpirationDate, AccessType, TicketType FROM [dbo].[TransactionsTicket] WHERE TransactionID = " + temp_tr[i].TransactionID, conn);
                    results = selectCommand.ExecuteReader();   

                    while(results.Read()){
                        temp_tr[i].expirationDate = results["ExpirationDate"].ToString()!;                        
                        temp_tr[i].accessType = results["AccessType"].ToString()!;
                        temp_tr[i].ticketType = results["TicketType"].ToString()!;               
                    }

                    
                    //get TicketLabel & access Table
                    selectCommand = new SqlCommand("SELECT a.AccessTypeLabel, b.TicketTypeLabel FROM dbo.Lookup_AccessType AS a, dbo.Lookup_TicketType AS b WHERE a.AccessType = '" + temp_tr[i].accessType + "' AND b.TicketType = '" + temp_tr[i].ticketType + "'", conn);
                    results = selectCommand.ExecuteReader();

                    while(results.Read()){                       
                        temp_tr[i].accessType = results["AccessTypeLabel"].ToString()!;
                        temp_tr[i].ticketType = results["TicketTypeLabel"].ToString()!;               
                    }                    
                    
                }         
            }
    
            tr_output = temp_tr;

            conn.Close();
        };            
    }
}
