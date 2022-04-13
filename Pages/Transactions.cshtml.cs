using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Data.SqlClient;

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
}

public class TicketTransactions{
    //TicketTransactions Entitiy attributes
    public int selectedAccess{get; set;} = default!; 
    public int selecetedTicket{get; set;} = default!;
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;
}

public class TransactionsModel : PageModel
{
    private readonly ILogger<TransactionsModel> _logger;
    private string connectionString = CSHolder.GetConnectionString();
    public static List<TransactionsOutput> tr_output = new List<TransactionsOutput>();

    public TransactionsModel(ILogger<TransactionsModel> logger)
    {
        _logger = logger;

    }

    public void OnGet()
    {
        GetTransactions();
//        Console.WriteLine("Database close");
    }

    private void SearchTransactions(){
        //set up sql query to change based on search variable
    }
    //needs to be tested
    private void GetTransactions()
    {
        //default query (nothing searched)
        if(tr_output.Count > 0){
            tr_output.Clear();
        }
        //Connect to database
        try{
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
    //            Console.WriteLine("Database open");
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Transactions]", conn);
                SqlDataReader results = selectCommand.ExecuteReader();

                List<TransactionsOutput> temp_tr = new List<TransactionsOutput>();

                //Get Main Transaction entity Info
                while(results.Read()){
                    temp_tr.Add(new TransactionsOutput{TransactionID = results["TransactionID"].ToString(),
                    itemID = results["Item"].ToString(),
                    date = results["Date"].ToString(),
                    price = results["Price"].ToString(),
                    IsTicket = results["IsTicket"].ToString(),
                    TicketID = results["TicketID"].ToString()});
                }

                //loop through transactions again to get all info needed
                for(int i = 0; i < temp_tr.Count; i++)
                {
                    //Get Item Label
                    int temp_itemID = Convert.ToInt32(temp_tr[i].itemID); 
                    selectCommand = new SqlCommand("SELECT itemLabel FROM [dbo].[Lookup_Item] WHERE item = " + temp_itemID, conn);
                    results = selectCommand.ExecuteReader();   
                    while(results.Read()){
                        temp_tr[i].itemLabel = results["itemLabel"].ToString();              
                    }

                    //Get Ticket Transactions entity info
                    if(Convert.ToBoolean(temp_tr[i].IsTicket) == false){
                        temp_tr[i].expirationDate = "NOT A TICKET";
                        temp_tr[i].TicketID = "NOT A TICKET";
                        temp_tr[i].accessType = "NOT A TICKET";
                        temp_tr[i].ticketType = "NOT A TICKET";                
                    }
                    else if(Convert.ToBoolean(temp_tr[i].IsTicket) == true){
                        temp_tr[i].itemLabel = "NOT AN ITEM";
                        //select from accesstable and tickettable
                        selectCommand = new SqlCommand("SELECT * FROM [dbo].[TransactionsTicket] " , conn);
                        results = selectCommand.ExecuteReader();   

                        while(results.Read()){
                            temp_tr[i].expirationDate = results["ExpirationDate"].ToString();                        
                            temp_tr[i].accessType = results["AccessType"].ToString();
                            temp_tr[i].ticketType = results["TicketType"].ToString();               
                        }                 
                        //get TicketLabel
                        /*
                        selectCommand = new SqlCommand("SELECT * FROM [dbo].[Lookup_TicketType],[dbo].[Lookup_TicketType] " , conn);
                        results = selectCommand.ExecuteReader();
                        */
                    }         
                }
        
                tr_output = temp_tr;

                conn.Close();
            };            
        }
        catch(Exception ex){
            throw ex;
        }
    }
}





