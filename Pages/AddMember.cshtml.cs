using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        
    }

}