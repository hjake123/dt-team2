using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;

public class TicketReportOutput
{
    public string DateFrom { get; set; } = default!;
    public string DateTo { get; set; } = default!;
    public string AccessType { get; set; } = default!;
    public string TicketType { get; set; } = default!;
    public string TotalRevenue{get; set;} = default!;
    public string TotalSales{get; set;} = default!;

}
public class TicketReport
{
    public DateTime DateFrom { get; set; } = default!;
    public DateTime DateTo { get; set; } = default!;
    public int AccessType { get; set; } = default!;
    public int TicketType { get; set; } = default!;
}

public class TicketReportModel : PageModel
{
    private readonly ILogger<TicketReportModel> _logger;

    public static List<TicketReportOutput> ticket_output{get; set;} = new List<TicketReportOutput>();
    public TicketReportModel(ILogger<TicketReportModel> logger)
    {
        _logger = logger;
        access = GetAccess();
        ticket = GetTicket();
    }
    public void OnGet(){
        ticket_output = new List<TicketReportOutput>();
    }    
    public DateTime DateFrom { get; set; } = default!;
    public DateTime DateTo { get; set; } = default!;
    public int AccessType { get; set; } = default!;
    public List<SelectListItem> access{get; set;} = new List<SelectListItem>();
    public int TicketType { get; set; } = default!;
    public List<SelectListItem> ticket{get; set;} = new List<SelectListItem>();
    public void OnPost(TicketReport t){
        DateFrom = t.DateFrom;
        DateTo = t.DateTo;
        AccessType = t.AccessType;
        TicketType = t.TicketType;
        Console.WriteLine(AccessType);
        Console.WriteLine(TicketType);

        if(DateFrom != default! && DateTo != default!){
            Console.WriteLine("Generating Ticket Sales Report......");
            string connectionString = CSHolder.GetConnectionString();

            using(SqlConnection conn = new SqlConnection(connectionString)){ //Access Type chosen
                conn.Open();
                if(AccessType != 0 && TicketType == 0){

                    SqlCommand selectCommand = new SqlCommand("SELECT SUM(a.Price) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.AccessType = " + AccessType, conn);
                    string tmp_totalRevenue  = selectCommand.ExecuteScalar().ToString()!; 
                    
                    selectCommand = new SqlCommand("SELECT Count(*) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.AccessType = " + AccessType, conn);
                    string tmp_totalSales  = selectCommand.ExecuteScalar().ToString()!;    

                    selectCommand = new SqlCommand("SELECT AccessTypeLabel FROM dbo.Lookup_AccessType WHERE AccessType = " + AccessType, conn);
                    string tmp_AccessType  = selectCommand.ExecuteScalar().ToString()!;                  

                    ticket_output.Add(new TicketReportOutput{DateFrom = DateFrom.ToString(), DateTo = DateTo.ToString(), AccessType = tmp_AccessType, TicketType = "------", TotalSales = tmp_totalSales, TotalRevenue = tmp_totalRevenue});   
                }
                if(AccessType == 0 && TicketType != 0){ //Ticket Type Chosen

                    SqlCommand selectCommand = new SqlCommand("SELECT SUM(a.Price) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.TicketType = " + TicketType, conn);
                    string tmp_totalRevenue  = selectCommand.ExecuteScalar().ToString()!; 
                    
                    selectCommand = new SqlCommand("SELECT Count(*) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.TicketType = " + TicketType, conn);
                    string tmp_totalSales  = selectCommand.ExecuteScalar().ToString()!;    

                    selectCommand = new SqlCommand("SELECT TicketTypeLabel FROM dbo.Lookup_TicketType WHERE TicketType = " + TicketType, conn);
                    string tmp_TicketType  = selectCommand.ExecuteScalar().ToString()!;                  

                    ticket_output.Add(new TicketReportOutput{DateFrom = DateFrom.ToString(), DateTo = DateTo.ToString(), AccessType = "------", TicketType = tmp_TicketType, TotalSales = tmp_totalSales, TotalRevenue = tmp_totalRevenue});   
                }
                if(AccessType != 0 && TicketType != 0){
                    
                    SqlCommand selectCommand = new SqlCommand("SELECT SUM(a.Price) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.AccessType = " + AccessType + "AND b.TicketType = " + TicketType, conn);
                    string tmp_totalRevenue  = selectCommand.ExecuteScalar().ToString()!; 
                    
                    selectCommand = new SqlCommand("SELECT Count(*) FROM dbo.Transactions AS a, dbo.TransactionsTicket AS b WHERE b.TicketID = a.TicketID AND b.AccessType = " + AccessType + " AND b.TicketType = " + TicketType, conn);
                    string tmp_totalSales  = selectCommand.ExecuteScalar().ToString()!;    


                    //change into one query later
                    selectCommand = new SqlCommand("SELECT AccessTypeLabel FROM dbo.Lookup_AccessType WHERE AccessType = " + AccessType, conn);
                    string tmp_AccessType  = selectCommand.ExecuteScalar().ToString()!; 

                    selectCommand = new SqlCommand("SELECT TicketTypeLabel FROM dbo.Lookup_TicketType WHERE TicketType = " + TicketType, conn);
                    string tmp_TicketType  = selectCommand.ExecuteScalar().ToString()!;                     

                    ticket_output.Add(new TicketReportOutput{DateFrom = DateFrom.ToString(), DateTo = DateTo.ToString(), AccessType = tmp_AccessType, TicketType = tmp_TicketType, TotalSales = tmp_totalSales, TotalRevenue = tmp_totalRevenue});   
                }
          
                conn.Close();
            }
            Console.WriteLine("Ticket Sales Report Completed !");
        }
//        Console.WriteLine("Ticket Type: " + TicketType);
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
}

