using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dt_team2.Pages;


public class MuseumReportOutputChange{
    public int TransactionsGained{get; set;} = default!;
    public int TotalPiecesGained{get; set;} = default!;
    public int TotalExhibitionsGained{get; set;} = default!;
}

public class MuseumReportOutput{
    public DateTime DateFrom{get; set;} = default!;
    public DateTime DateTo{get; set;} = default!;
    public string TotalTransactions{get; set;} = default!;
    public string TotalPieces{get; set;} = default!;
    public string TotalExhibitions{get; set;} = default!;
}

public class MuseumReport{
    public DateTime CompareDateFrom{get; set;} = default!;
    public DateTime CompareDateTo{get; set;} = default!;
    public DateTime WithDateFrom{get; set;} = default!;
    public DateTime WithDateTo{get; set;} = default!;    
}

public class MuseumReportModel : PageModel
{
    private readonly ILogger<MuseumReportModel> _logger;
    public MuseumReportModel(ILogger<MuseumReportModel> logger)
    {
        _logger = logger;
    }
    public void OnGet(){
        museum_output = new List<MuseumReportOutput>();
        museum_output_change = new List<MuseumReportOutputChange>();
    }
    public static List<MuseumReportOutput> museum_output{get; set;} = new List<MuseumReportOutput>();   
    public static List<MuseumReportOutputChange> museum_output_change{get; set;} = new List<MuseumReportOutputChange>();   
    public DateTime CompareDateFrom{get; set;} = default!;
    public DateTime CompareDateTo{get; set;} = default!;

    public DateTime WithDateFrom{get; set;} = default!;
    public DateTime WithDateTo{get; set;} = default!;

    public void OnPost(MuseumReport mr)
    {
        CompareDateFrom = mr.CompareDateFrom;
        CompareDateTo = mr.CompareDateTo;
        WithDateFrom = mr.WithDateFrom;
        WithDateTo = mr.WithDateTo;   

        if(CompareDateFrom != default! && CompareDateTo != default! && WithDateTo != default! && WithDateFrom != default!){
            Console.WriteLine("Generating Report .......");
            string connectionString = CSHolder.GetConnectionString();

            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                //Get to transactions From and To
                SqlCommand selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Transactions WHERE Date BETWEEN '" + CompareDateFrom +"' AND '" + CompareDateTo + "'", conn);
                Console.WriteLine("Comparing Date From: " + CompareDateFrom);
                Console.WriteLine("To: " + CompareDateTo);
                string tmp_tAdded = selectCommand.ExecuteScalar().ToString()!;      

                selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.ArtPieces WHERE DateSubmitted BETWEEN '" + CompareDateFrom +"' AND '" + CompareDateTo + "'", conn);
                string tmp_pAdded = selectCommand.ExecuteScalar().ToString()!;   

                selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Exhibitions WHERE DateEnd BETWEEN '" + CompareDateFrom +"' AND '" + CompareDateTo + "'", conn);
                string tmp_eAdded = selectCommand.ExecuteScalar().ToString()!;   
                
                museum_output.Add(new MuseumReportOutput{DateFrom = CompareDateFrom, DateTo = CompareDateTo, TotalTransactions = tmp_tAdded, TotalPieces = tmp_pAdded, TotalExhibitions = tmp_eAdded});        


                Console.WriteLine("With Date From:" + WithDateFrom);
                Console.WriteLine("To:" + WithDateTo);

                selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Transactions WHERE Date BETWEEN '" + WithDateFrom +"' AND '" + WithDateTo + "'", conn);
                Console.WriteLine("Comparing Date From: " + CompareDateFrom);
                Console.WriteLine("To: " + CompareDateTo);
                string tmp_tAdded_w = selectCommand.ExecuteScalar().ToString()!;  

                selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.ArtPieces WHERE DateSubmitted BETWEEN '" + WithDateFrom +"' AND '" + WithDateTo + "'", conn);
                string tmp_pAdded_w = selectCommand.ExecuteScalar().ToString()!;

                selectCommand = new SqlCommand("SELECT COUNT(*) FROM dbo.Exhibitions WHERE DateEnd BETWEEN '" + WithDateFrom +"' AND '" + WithDateTo + "'", conn);
                string tmp_eAdded_w = selectCommand.ExecuteScalar().ToString()!;   

                museum_output.Add(new MuseumReportOutput{DateFrom = WithDateFrom, DateTo = WithDateTo, TotalTransactions = tmp_tAdded_w, TotalPieces = tmp_pAdded_w, TotalExhibitions = tmp_eAdded_w});

                int tmp_transactionsGained = Int32.Parse(tmp_tAdded_w) - Int32.Parse(tmp_tAdded);
                int tmp_piecesGained = Int32.Parse(tmp_pAdded_w) - Int32.Parse(tmp_pAdded);
                int tmp_exhibitionsAdded = Int32.Parse(tmp_eAdded_w) - Int32.Parse(tmp_eAdded);


                museum_output_change.Add(new MuseumReportOutputChange{TransactionsGained = tmp_transactionsGained, TotalPiecesGained = tmp_piecesGained, TotalExhibitionsGained = tmp_exhibitionsAdded});

                conn.Close();
            }
        }

 
        Console.WriteLine("Report Generated !");

    }
}
