using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class LookUp_TicketType{
    public int ticketTypeID{get; set;} = default!;    
    public string ticketTypeLabel{get; set;} = default!;

}

public class LookUp_AccessType{
    public int accessTypeID{get; set;} = default!;    
    public string accessTypeLabel{get; set;} = default!;

}

public class Lookup_Item{
    public int itemID{get; set;} = default!;    
    public string itemLabel{get; set;} = default!;

}