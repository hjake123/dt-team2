using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class AddMemberModel : PageModel
{
    private readonly ILogger<AddMemberModel> _logger;
 
    public AddMemberModel(ILogger<AddMemberModel> logger)
    {
        _logger = logger;
    }
    public int memberID{get; set;} = default!;
    public string firstName{get; set;} = default!;
    public string lastName{get; set;} = default!;
    public int cardNumber{get; set;} = default!;
    public DateTime lastVisit{get; set;} = default!;    
    public string output_msg = default!;
    public void OnPost(Members m){
        memberID = m.memberID;
        firstName = m.firstName;
        lastName = m.lastName;
        cardNumber = m.cardNumber;
        lastVisit = m.lastVisit;

        Console.WriteLine("FIRST NAME: " + firstName);
        Console.WriteLine("LAST NAME: " + lastName);
        Console.WriteLine("CARD NUMBER: " + cardNumber);
        Console.WriteLine("LAST VISIT: " + lastVisit);

        string connectionString = CSHolder.GetConnectionString();        

        int tmpMemberID = GenerateID();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Members(MemberID, FirstName, LastName, CardNumber, LastVisit) VALUES( @tmpMemberID, @firstName, @lastName, @cardNumber, @lastVisit)", conn);
            selectCommand.Parameters.Add(new SqlParameter("tmpMemberID", tmpMemberID));
            selectCommand.Parameters.Add(new SqlParameter("firstName", firstName));
            selectCommand.Parameters.Add(new SqlParameter("lastName", lastName));
            selectCommand.Parameters.Add(new SqlParameter("cardNumber", cardNumber));
            selectCommand.Parameters.Add(new SqlParameter("lastVisit", lastVisit)); 
                            
            try{
                selectCommand.ExecuteNonQuery();
            } catch(SqlException e)
            {
                output_msg = e.Message.ToString();
            }
            conn.Close();
        }        

    }

    private bool DoesIDExist(int M_ID){
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT MemberID FROM dbo.Members WHERE MemberID = " + M_ID, conn); 
            SqlDataReader results = selectCommand.ExecuteReader();  

            while(results.Read()){
                if(results["MemberID"].ToString() == M_ID.ToString()){

                    conn.Close();
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

        Console.WriteLine("Generating ID.....");
        //if T_ID already exists rnd.next(); constantyl check
        while(DoesIDExist(M_ID) == true){
            Console.WriteLine("There exists one ID for " + M_ID);
            M_ID = rnd.Next();
            Console.WriteLine("Generating New ID....");                
        }

        Console.WriteLine("Success Generating ID " + M_ID);    
        return M_ID;
    }
    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }
}