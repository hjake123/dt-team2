using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class Exhibition {

	public void OnGet() {}
    public string ExhibitionName { get; set; }
    public string Description { get; set; }
    public string PiecesIncluded { get; set; }
    public string Arranger { get; set; }
    public string Location { get; set; }
    public string DatesOpen { get; set; }
}
