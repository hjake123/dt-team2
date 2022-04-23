using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace dt_team2.Pages;


public class SellATicketModel : PageModel
{
    private readonly ILogger<SellATicketModel> _logger;

    public SellATicketModel(ILogger<SellATicketModel> logger)
    {
        _logger = logger;
        access = GetAccess();
        ticket = GetTicket();
    } 
    //Access & Ticket Lists---------------------------------------------------
    public int selectedAccess{get; set;} = default!;
    public List<SelectListItem> access{get; set;} = new List<SelectListItem>();

    public int selectedTicket{get; set;} = default!;
    public List<SelectListItem> ticket{get; set;} = new List<SelectListItem>();
    //Other-----------------------------------------------------------------
    public float price{get; set;} = default!;
    public DateTime expirationDate{get; set;} = default!;
    public DateTime date{get; set;} = default!;

    //Functions------------------------------------------------------------
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

    public void OnPost(Transactions tr, TicketTransactions tick){
        selectedAccess = tick.selectedAccess;
        selectedTicket = tick.selectedTicket;
        price = tick.price;
        expirationDate = tick.expirationDate;

        date = tr.date;
        //connect insert into database
        string connectionString = CSHolder.GetConnectionString();

        int temp_transactionID = GenerateID();

        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();


            //insert into transaction
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Transactions (TransactionID, Item, Date, Price, IsTicket) VALUES( @temp_transactionID, 1, @date, @price, 'TRUE')", conn);
            selectCommand.Parameters.Add(new SqlParameter("temp_transactionID",temp_transactionID));
            selectCommand.Parameters.Add(new SqlParameter("date",date));              
            selectCommand.Parameters.Add(new SqlParameter("price",price));

            selectCommand.ExecuteNonQuery();

            //insert into Transactions Ticket
            selectCommand = new SqlCommand("INSERT INTO dbo.TransactionsTicket (ExpirationDate, AccessType, TicketType, TransactionID) VALUES( @expirationDate, @selectedAccess, @selectedTicket, @temp_transactionID)", conn);
            selectCommand.Parameters.Add(new SqlParameter("expirationDate",expirationDate));
            selectCommand.Parameters.Add(new SqlParameter("selectedAccess", selectedAccess));              
            selectCommand.Parameters.Add(new SqlParameter("selectedTicket", selectedTicket));
            selectCommand.Parameters.Add(new SqlParameter("temp_transactionID",temp_transactionID));

            selectCommand.ExecuteNonQuery();

            //get ticket ID for insertion into transactionsTicket
            selectCommand = new SqlCommand("SELECT TicketID FROM dbo.TransactionsTicket WHERE TransactionID = " + temp_transactionID, conn); 
            SqlDataReader results = selectCommand.ExecuteReader();    

            string tmp_ticketID = default!;

            while(results.Read()){
                tmp_ticketID = results["TicketID"].ToString()!;
            }
            
            //update transaction
            selectCommand = new SqlCommand("UPDATE dbo.Transactions SET TicketID = @tmp_ticketID WHERE TransactionID = @temp_transactionID", conn);
            selectCommand.Parameters.Add(new SqlParameter("tmp_ticketID",tmp_ticketID));
            selectCommand.Parameters.Add(new SqlParameter("temp_transactionID",temp_transactionID));

            selectCommand.ExecuteNonQuery();            

            conn.Close();
        }
                
        Console.WriteLine("Ticket Sold!");
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
    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }
}


