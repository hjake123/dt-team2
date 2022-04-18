using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace dt_team2.Pages;

public class MemberEditModel : PageModel
{
    private readonly ILogger<MemberEditModel> _logger;
 
    public MemberEditModel(ILogger<MemberEditModel> logger)
    {
        _logger = logger;
        members_output = GetMembers();
    }
    public static List<MembersOutput> members_output{get; set;} = new List<MembersOutput>();    
    public int memberID{get; set;} = default!;
    public string firstName{get; set;} = default!;
    public string lastName{get; set;} = default!;
    public int cardNumber{get; set;} = default!;
    public DateTime lastVisit{get; set;} = default!;    

    public void OnPost(Members m){
        memberID = m.memberID;
        firstName = m.firstName;
        lastName = m.lastName;
        cardNumber = m.cardNumber;
        lastVisit = m.lastVisit;

        if(memberID != 0){
            if(firstName != default!){
                //connect to database
                string connectionString = CSHolder.GetConnectionString();
                
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Members SET FirstName = '" + firstName + "' WHERE MemberID = " + memberID, conn);
                    selectCommand.ExecuteNonQuery();
                    
                    conn.Close();
                }
                Console.WriteLine("FIRST NAME CHANGED TO: " + firstName);              
            }
            if(lastName != default!){
                string connectionString = CSHolder.GetConnectionString();
                
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Members SET LastName = '" + lastName + "' WHERE MemberID = " + memberID, conn);
                    selectCommand.ExecuteNonQuery();
                    
                    conn.Close();
                }
                Console.WriteLine("LAST NAME CHANGED TO: " + lastName);                 
            }
            if(cardNumber != default!){
                string connectionString = CSHolder.GetConnectionString();
                
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Members SET CardNumber = '" + cardNumber + "' WHERE MemberID = " + memberID, conn);
                    selectCommand.ExecuteNonQuery();
                    
                    conn.Close();
                }
                Console.WriteLine("CARD NUMBER CHANGED TO: " + cardNumber);                 
            }
            if(lastVisit != default!){

                string connectionString = CSHolder.GetConnectionString();
                
                using(SqlConnection conn = new SqlConnection(connectionString)){
                    conn.Open();
                    SqlCommand selectCommand = new SqlCommand("UPDATE dbo.Members SET LastVisit = '" + lastVisit + "' WHERE MemberID = " + memberID, conn);
                    selectCommand.ExecuteNonQuery();
                    
                    conn.Close();
                }
                Console.WriteLine("LAST VISIT CHANGED TO: " + lastVisit);        
            }
            Console.WriteLine("MemberID Edited: " + memberID);  
        }
        Response.Redirect("Members");
    }   

    private List<MembersOutput> GetMembers(){
        List<MembersOutput> tmp_members = new List<MembersOutput>();

        //connect to database
        string connectionString = CSHolder.GetConnectionString();
        
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Members]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tmp_members.Add(new MembersOutput{memberID = results["MemberID"].ToString()!, firstName = results["FirstName"].ToString()!, 
                lastName = results["LastName"].ToString()!, cardNumber = results["CardNumber"].ToString()!, lastVisit =results["LastVisit"].ToString()!});  
            }
            
            conn.Close();
        }

        return tmp_members;        
    }
}