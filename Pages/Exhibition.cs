using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class Exhibition {

	public void OnGet() {}
    public string ExhibitionName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PiecesIncluded { get; set; } = default!;
    public string Arranger { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string DatesOpen { get; set; } = default!;
}
