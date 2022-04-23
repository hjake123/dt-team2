using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

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
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
    }


    public string ExhibitionName { get; set; } = default!;
    public string Description { get; set; } = default!;
    // public string ListOfPieces { get; set; } = default!;
    public string Arranger { get; set; } = default!;
    public string Location { get; set; } = default!;
    public DateTime DateEnd { get; set; } = default!;

    string d = "', '";

    // submit form to OnPost
    public IActionResult OnPost(Exhibition exhibition) {
	    ExhibitionName = exhibition.ExhibitionName;
	    Description = exhibition.Description;
	    // ListOfPieces = exhibition.ListOfPieces;
	    Arranger = exhibition.Arranger;
	    Location = exhibition.Location;
	    DateEnd = exhibition.DateEnd;
	    String[] fields = {ExhibitionName, Description, Arranger, Location};
	    if (DateEnd == new DateTime(1,1,1)) ModelState.AddModelError("DateEnd", "Please set a date for the exhibition to end!");
			else if (DateEnd < DateTime.Today) ModelState.AddModelError("DateEnd", "Exhibition must end after today!");
	    else if ((new String[] {ExhibitionName, Description, Arranger, Location}.All(field => !String.IsNullOrEmpty(field)))) {
		    using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			    connection.Open();
			    SqlCommand select = new SqlCommand("INSERT INTO dbo.Exhibitions(ExhibitionName, Description, Arranger, Location, DateEnd) VALUES(@ExhibitionName ,@Description ,@Arranger ,@Location ,@DateEnd )", connection);
					select.Parameters.Add(new SqlParameter("ExhibitionName",ExhibitionName));
					select.Parameters.Add(new SqlParameter("Description",Description));
					select.Parameters.Add(new SqlParameter("Arranger",Arranger));
					select.Parameters.Add(new SqlParameter("Location",Location));
					select.Parameters.Add(new SqlParameter("DateEnd",DateEnd));
			    int rows_added = select.ExecuteNonQuery();
			    connection.Close();
			    Console.WriteLine(rows_added + " Exhibition added");
		    }
		    return RedirectToPage("/Exhibitions");
	    }
	    return Page();
    }

}