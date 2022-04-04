using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class AddExhibitionModel : PageModel
{
    private readonly ILogger<AddExhibitionModel> _logger;

    public AddExhibitionModel(ILogger<AddExhibitionModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }


    public string ExhibitionName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PiecesIncluded { get; set; } = default!;
    public string Arranger { get; set; } = default!;
    public string Location { get; set; } = default!;
    public string DatesOpen { get; set; } = default!;

    // [HttpPost] 
    public ActionResult Exhibition_DE(Exhibition exhibition) {

        string ExhibitionName = exhibition.ExhibitionName;
        string Description = exhibition.Description;
        string PiecesIncluded = exhibition.PiecesIncluded;
        string Arranger = exhibition.Arranger;
        string Location = exhibition.Location;
        string DatesOpen = exhibition.DatesOpen;


        return null;
    }

    // submit form to OnPost
    public void OnPost(Exhibition exhibition) {

        string ExhibitionName = exhibition.ExhibitionName;
        string Description = exhibition.Description;
        string PiecesIncluded = exhibition.PiecesIncluded;
        string Arranger = exhibition.Arranger;
        string Location = exhibition.Location;
        string DatesOpen = exhibition.DatesOpen;
    }


}
