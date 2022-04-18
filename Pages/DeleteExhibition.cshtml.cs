using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class DeleteExhibitionModel : PageModel {
	private readonly ILogger<DeleteExhibitionModel> _logger;

	public DeleteExhibitionModel(ILogger<DeleteExhibitionModel> logger) {
		_logger = logger;
	}

	public void OnGet(string id) {
		ExhibitionName = id;
  }


	public string ExhibitionName {
		get; set;
	} = default !;
	public string Description {
		get; set;
	} = default !;
	// public string ListOfPieces { get; set; } = default!;
	public string Arranger {
		get; set;
	} = default !;
	public string Location {
		get; set;
	} = default !;
	public DateTime DateEnd {
		get; set;
	} = default !;

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
		if (DateEnd == new DateTime(1,1,1)) {
			ModelState.AddModelError("DateEnd", "Please set a date for the exhibition to end!");
		}
		else if ((new String[] {ExhibitionName, Description, Arranger, Location}.All(field => !String.IsNullOrEmpty(field)))) {
			using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
				connection.Open();
				SqlCommand select = new SqlCommand("INSERT INTO dbo.Exhibitions(ExhibitionName, Description, Arranger, Location, DateEnd) VALUES('" + ExhibitionName + d + Description + d + Arranger + d + Location + d + DateEnd + "')", connection);
				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Exhibition added");
			}
			return RedirectToPage("/Exhibitions");
		}
		return Page();
	}

}