using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class EditExhibitionModel : PageModel {
	private readonly ILogger<EditExhibitionModel> _logger;

	public Exhibition exhibition = new Exhibition();

	public EditExhibitionModel(ILogger<EditExhibitionModel> logger) {
		_logger = logger;
	}

	public void OnGet(string id) {

		OrigExhibitionName = id;

		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			connection.Open();
			SqlCommand select = new SqlCommand("SELECT ExhibitionName, Description, Arranger, Location, DateEnd FROM dbo.Exhibitions WHERE ExhibitionName=@OrigExhibitionName", connection);
			select.Parameters.Add(new SqlParameter("OrigExhibitionName", OrigExhibitionName));
			SqlDataReader data = select.ExecuteReader();

			if (data.Read()) {
				exhibition = new Exhibition {
					OrigExhibitionName = data["ExhibitionName"].ToString()!,
					ExhibitionName = data["ExhibitionName"].ToString() !,
					Description = data["Description"].ToString() !,
					// ListOfPieces = data["ListOfPieces"].ToString()!,
					Arranger = data["Arranger"].ToString() !,
					Location = data["Location"].ToString() !,
					DateEnd = (DateTime)data["DateEnd"],
				};
			}
			connection.Close();
		}
  }

	public string OrigExhibitionName {
		get; set;
	} = default !;
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


	// submit form to OnPost
	public IActionResult OnPost(Exhibition exhibition) {
		OrigExhibitionName = exhibition.OrigExhibitionName;
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
				SqlCommand select = new SqlCommand("UPDATE dbo.Exhibitions SET ExhibitionName = @ExhibitionName, Description = @Description, Arranger = @Arranger, Location = @Location, DateEnd = @DateEnd WHERE ExhibitionName=@OrigExhibitionName", connection);
				select.Parameters.Add(new SqlParameter("OrigExhibitionName", OrigExhibitionName));
				select.Parameters.Add(new SqlParameter("ExhibitionName", ExhibitionName));
				select.Parameters.Add(new SqlParameter("Description", Description));
				select.Parameters.Add(new SqlParameter("Arranger", Arranger));
				select.Parameters.Add(new SqlParameter("Location", Location));
				select.Parameters.Add(new SqlParameter("DateEnd", DateEnd));
				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Exhibition edited");
			}
			return RedirectToPage("/Exhibitions");
		}
		return Page();
	}

}