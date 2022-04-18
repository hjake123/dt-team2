using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class EditCollectionModel : PageModel {
	private readonly ILogger<EditCollectionModel> _logger;

	public EditCollectionModel(ILogger<EditCollectionModel> logger) {
		_logger = logger;
	}

	public void OnGet(string id) {
		OrigCollectionName = id;
		// sql fetch, populate fields
		
    // POPULATE HERE
  }


	public string OrigCollectionName {
		get; set;
	} = default !;
	public string CollectionName {
		get; set;
	} = default !;
	public string Description {
		get; set;
	} = default !;
	string d = "', '";

	// submit form to OnPost
	public IActionResult OnPost(Collection collection) {
		OrigCollectionName = collection.OrigCollectionName;
		CollectionName = collection.CollectionName;
		Description = collection.Description;
		String[] fields = {CollectionName, Description};
		if ((new String[] {CollectionName, Description}.All(field => !String.IsNullOrEmpty(field)))) {
			using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
				connection.Open();
				SqlCommand select = new SqlCommand("UPDATE dbo.Collections SET CollectionName = '" + CollectionName + "', Description = '" + Description + "' WHERE CollectionName='" + OrigCollectionName + "' ", connection);
				Console.WriteLine(select.CommandText);
				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Collection added");
			}
			return RedirectToPage("/Collections");
		}
		return Page();
	}

}