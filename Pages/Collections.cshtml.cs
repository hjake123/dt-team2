using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class CollectionsModel : PageModel {
	private readonly ILogger<CollectionsModel> _logger;
	public static List<Collection> collections = new List<Collection>();
	private string c_string = CSHolder.GetConnectionString();

	public CollectionsModel(ILogger<CollectionsModel> logger) {
		_logger = logger;
	}

	public void GetCollections() {
		if (collections.Count > 0) collections.Clear();

		try {
			using (SqlConnection connection = new SqlConnection(c_string)) {
				connection.Open();
				SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Collections]", connection);
				SqlDataReader data = selectCommand.ExecuteReader();
				List<Collection> list = new List<Collection>();

				while (data.Read()) {
					list.Add(new Collection {
						CollectionName = data["CollectionName"].ToString() !,
						Description = data["Description"].ToString() !,
					});
				}
				collections = list;

				connection.Close();
			};
		}
		catch (Exception) { throw; }
	}

	public void OnGet() {
		if(Request.Cookies["session_user"] == null) {
			// Then no session cookie exists and they're not logged in! Get 'em out of here!
			Response.Redirect("Login");
		}
		GetCollections();
	}

	public string Search {
		get; set;
	} = default !;
}
