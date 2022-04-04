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

    public string ExhibitionName { get; set; }
    public string Description { get; set; }
    public string PiecesIncluded { get; set; }
    public string Arranger { get; set; }
    public string Location { get; set; }
    public string DatesOpen { get; set; }

    [HttpPost] 
    public ActionResult Exhibition_DE(Exhibition exhibition) {

        string ExhibitionName = exhibition.ExhibitionName;
        string Description = exhibition.Description;
        string PiecesIncluded = exhibition.PiecesIncluded;
        string Arranger = exhibition.Arranger;
        string Location = exhibition.Location;
        string DatesOpen = exhibition.DatesOpen;


        return null;
    }


}
