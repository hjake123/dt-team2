using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class Collection {

	public void OnGet() {}
    public string CollectionName { get; set; } = default!;
    public string Description { get; set; } = default!;
}
