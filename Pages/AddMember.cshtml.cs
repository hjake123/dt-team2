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

    public void OnPost(Members m) {
        firstName = m.firstName;
        lastName = m.lastName;
        cardNumber = m.cardNumber;
        lastVisit = m.lastVisit;
        
        //connect to database
        
        // REMEMBER: && ALREADY EXISTS FUNCTION (Create function that checks database against existing values)
        string connectionString = CSHolder.GetConnectionString();
        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("INSERT INTO dbo.Members(FirstName, LastName, CardNumber, LastVisit) VALUES ('" + firstName + "', '" + lastName + "', '" + cardNumber + "', '" + lastVisit +"')", conn);      
            selectCommand.ExecuteNonQuery();
            conn.Close();
        }
        Console.WriteLine("Member Added");
    
    }
 //   public string Search { get; set; }
}