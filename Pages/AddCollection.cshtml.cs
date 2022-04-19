using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class AddCollectionModel : PageModel {
	private readonly ILogger<AddCollectionModel> _logger;

	public AddCollectionModel(ILogger<AddCollectionModel> logger) {
		_logger = logger;
	}

	public void OnGet() {}


	public string CollectionName {
		get; set;
	} = default !;
	public string Description {
		get; set;
	} = default !;	string d = "', '";

	// submit form to OnPost
	public IActionResult OnPost(Collection collection) {
		CollectionName = collection.CollectionName;
		Description = collection.Description;
		String[] fields = {CollectionName, Description};
		if ((new String[] {CollectionName, Description}.All(field => !String.IsNullOrEmpty(field)))) {
			using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
				connection.Open();
				// SqlCommand select = new SqlCommand("INSERT INTO dbo.Collections(CollectionName, Description) VALUES('" + CollectionName + d + Description + "')", connection);
				SqlCommand select = new SqlCommand("INSERT INTO dbo.Collections(CollectionName, Description) VALUES(@CollectionName, @Description)", connection);
				select.Parameters.Add(new SqlParameter("CollectionName", CollectionName));
				select.Parameters.Add(new SqlParameter("Description", Description));

				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Collection added");
			}
			return RedirectToPage("/Collections");
		}
		return Page();
	}

}