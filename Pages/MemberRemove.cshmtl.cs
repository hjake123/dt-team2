using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class MemberRemoveModel : PageModel
{
    private readonly ILogger<MemberRemoveModel> _logger;
    public MemberRemoveModel(ILogger<MemberRemoveModel> logger)
    {
        _logger = logger;
        members_output = GetMembers();
    }
    public static List<MembersOutput> members_output{get; set;} = new List<MembersOutput>();
    public int memberID = default!;

    public void OnPost(Members m) {
        memberID = m.memberID;

        Console.WriteLine("Attemp to Remove Member ID: " + memberID);

        //remove query database
        if(memberID != 0)
        {
            string connectionString = CSHolder.GetConnectionString();
            
            using(SqlConnection conn = new SqlConnection(connectionString)){
                conn.Open();
                SqlCommand selectCommand = new SqlCommand("DELETE FROM dbo.Members WHERE MemberID = @memberID", conn);
                selectCommand.Parameters.Add(new SqlParameter("memberID", memberID));
                selectCommand.ExecuteNonQuery();
                
                conn.Close();
            }            
            Console.WriteLine("Successfully Removed Member ID: " + memberID);
            Response.Redirect("Members");
        }

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
    public void OnGet()
    {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
        
		if(Request.Cookies["session_user_role"] != "admin"){
			// Only admins can delete items!
            Response.Redirect("ForbiddenAction");
        }
    }
}