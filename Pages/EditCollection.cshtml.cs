using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class EditCollectionModel : PageModel {
	private readonly ILogger<EditCollectionModel> _logger;

	public Collection collection = new Collection();

	public EditCollectionModel(ILogger<EditCollectionModel> logger) {
		_logger = logger;
	}

	public void OnGet(string id) {
		OrigCollectionName = id;

		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			connection.Open();
			SqlCommand select = new SqlCommand("SELECT CollectionName, Description FROM dbo.Collections WHERE CollectionName=@CollectionName", connection);
			select.Parameters.Add(new SqlParameter("CollectionName", OrigCollectionName));

			SqlDataReader data = select.ExecuteReader();
			if (data.Read()) {
				collection = new Collection {
					OrigCollectionName = data["CollectionName"].ToString() !,
					CollectionName = data["CollectionName"].ToString() !,
					Description = data["Description"].ToString() !,
				};
			}
			connection.Close();
		}

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

	// submit form to OnPost
	public IActionResult OnPost(Collection collection) {
		OrigCollectionName = collection.OrigCollectionName;
		CollectionName = collection.CollectionName;
		Description = collection.Description;
		String[] fields = {CollectionName, Description};
		if ((new String[] {CollectionName, Description}.All(field => !String.IsNullOrEmpty(field)))) {
			using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
				connection.Open();
				SqlCommand select = new SqlCommand("UPDATE dbo.Collections SET CollectionName = @CollectionName, Description = @Description WHERE CollectionName=@OrigCollectionName", connection);
				select.Parameters.Add(new SqlParameter("CollectionName", CollectionName));
				select.Parameters.Add(new SqlParameter("Description", Description));
				select.Parameters.Add(new SqlParameter("OrigCollectionName", OrigCollectionName));
				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Collection edited");
			}
			return RedirectToPage("/Collections");
		}
		return Page();
	}

}