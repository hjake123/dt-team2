using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class Members{
    public int memberID{get; set;} = default!;
    public string firstName{get; set;} = default!;
    public string lastName{get; set;} = default!;
    public int cardNumber{get; set;} = default!;
    public DateTime lastVisit{get; set;} = default!;
}

public class MembersOutput{
    public string memberID = default!;
    public string firstName = default!;
    public string lastName = default!;
    public string cardNumber = default!;
    public string lastVisit = default!;    
}

public class MembersModel : PageModel
{
    private readonly ILogger<MembersModel> _logger;
    public MembersModel(ILogger<MembersModel> logger)
    {
        _logger = logger;
        members_output = GetMembers();
    }
    public static List<MembersOutput> members_output{get; set;} = new List<MembersOutput>();
    public int itemID = default!;
    public string itemLabel = default!;

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