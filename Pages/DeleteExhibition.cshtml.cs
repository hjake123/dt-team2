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

	// submit form to OnPost
	public IActionResult OnPost(Exhibition exhibition) {
		ExhibitionName = exhibition.ExhibitionName;
		
		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			connection.Open();
			SqlCommand select = new SqlCommand("DELETE FROM dbo.Exhibitions WHERE ExhibitionName='" + ExhibitionName + "'", connection);
			int rows_deleted = select.ExecuteNonQuery();
			connection.Close();
			Console.WriteLine(rows_deleted + " Exhibition deleted");
		}
			return RedirectToPage("/Exhibitions");
	}
}