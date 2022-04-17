using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;


public class Members{
    public string firstName = default!;
    public string lastName = default!;
    public int cardNumber = default!;
    public DateTime lastVisit = default!;    
}
public class AddMemberModel : PageModel
{
    private readonly ILogger<AddMemberModel> _logger;
 
    public AddMemberModel(ILogger<AddMemberModel> logger)
    {
        _logger = logger;
    }

    public string firstName = default!;
    public string lastName = default!;
    public int cardNumber = default!;
    public DateTime lastVisit = default!;

    private bool DoesIDExist(int M_ID){
        string connectionString = CSHolder.GetConnectionString();
        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT MemberID FROM dbo.Members WHERE MemberID = " + M_ID, conn); 
            SqlDataReader results = selectCommand.ExecuteReader();  

            while(results.Read()){
                if(results["TransactionID"].ToString() == M_ID.ToString()){
                    return true; 
                }
            }            
  
            conn.Close();
        }
        return false;
    }
    private int GenerateID(){
        Random rnd = new Random();
        int M_ID = rnd.Next();
        
        Console.WriteLine("Generating Member ID.....");
        //if T_ID already exists rnd.next(); constantyl check
        while(DoesIDExist(M_ID) == true){
            Console.WriteLine("There exists one Member ID for " + M_ID);
            M_ID = rnd.Next();
            Console.WriteLine("Generating New Member ID....");                
        }

        Console.WriteLine("Success Generating Member ID " + M_ID);    
        return M_ID;
    }

    public void OnPost(Members m) {
        firstName = m.firstName;
        lastName = m.lastName;
        cardNumber = m.cardNumber;
        lastVisit = m.lastVisit;
        
        //connect to database
        
        // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
        if(firstName != default! && lastName != default! && cardNumber != 0 && lastVisit != default!)
        {
            string connectionString = CSHolder.GetConnectionString();
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();

                int temp_MemberID = GenerateID();
                SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Members(MemberID, FirstName, LastName, CardNumber, LastVisit) VALUES (" + temp_MemberID + ", '" + firstName + "', '" + lastName + "', '" + cardNumber + "', '" + lastVisit +"')", conn);      
                selectCommand.ExecuteNonQuery();

                conn.Close();
            }
            Console.WriteLine("Member Added");
        }
    }
 //   public string Search { get; set; }
}