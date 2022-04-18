using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class DeleteCollectionModel : PageModel {
	private readonly ILogger<DeleteCollectionModel> _logger;

	public DeleteCollectionModel(ILogger<DeleteCollectionModel> logger) {
		_logger = logger;
	}

	public void OnGet(string id) {
		CollectionName = id;
  }

	public string CollectionName {
		get; set;
	} = default !;

	// submit form to OnPost
	public IActionResult OnPost(Collection collection) {
		CollectionName = collection.CollectionName;
		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			connection.Open();
			SqlCommand select = new SqlCommand("DELETE FROM dbo.Collections WHERE CollectionName='" + CollectionName + "'", connection);
			int rows_deleted = select.ExecuteNonQuery();
			connection.Close();
			Console.WriteLine(rows_deleted + " Collection deleted");
		}
			return RedirectToPage("/Collections");
	}
}