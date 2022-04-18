using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class AddCollectionModel : PageModel
{
    private readonly ILogger<AddCollectionModel> _logger;

    public AddCollectionModel(ILogger<AddCollectionModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }


    public string CollectionName { get; set; } = default!;
    public string Description { get; set; } = default!;

    // [HttpPost] 
    public ActionResult Exhibition_DE(Collection c) {

        string CollectionName = c.CollectionName;
        string Description = c.Description;

        return new EmptyResult();
    }

    // submit form to OnPost
    public void OnPost(Collection c) {

        string ExhibitionName = c.CollectionName;
        string Description = c.Description;
    }


}
