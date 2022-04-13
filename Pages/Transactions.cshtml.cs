using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;

namespace dt_team2.Pages;



public class Transactions{
    //Transactions Enitity Attributes
    public int TransactionID{get; set;} = default!;
    public string itemName{get; set;} = default!;
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
    string connectionString = CSHolder.GetConnectionString();
    SqlConnection conn;

    public TransactionsModel(ILogger<TransactionsModel> logger)
    {
        _logger = logger;
        conn = new SqlConnection(connectionString);
    }

    public void OnGet()
    {
        GetTransactions();
        Console.WriteLine("Database close");
    }

    private void SearchTransaction(){
        //set up sql query to change based on search bane
    }
    private void GetTransactions()
    {
        //default query (nothing searched)
        //Connect to database
        try{
            conn.Open();
            Console.WriteLine("Database open");
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Transactions]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();


            conn.Close();
        }
        catch(Exception ex){
            throw ex;
        }


        /*
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            // Connect to the database
            conn.Open();
            // Read rows
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[ArtPieces]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();
            
            // Enumerate over the rows
            while(results.Read())
            {
                Console.WriteLine("Column 0: {0}", results[0]);
            }

            conn.Close();
        }*/
    }
}





