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
		if(Request.Cookies["session_user_role"] != "admin"){
			// Only admins can delete items!
            Response.Redirect("ForbiddenAction");
        }
  	}

	public string ExhibitionName {
		get; set;
	} = default !;

	// submit form to OnPost
	public IActionResult OnPost(Exhibition exhibition) {
		ExhibitionName = exhibition.ExhibitionName;
		
		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString())) {
			connection.Open();
			SqlCommand select = new SqlCommand("DELETE FROM dbo.Exhibitions WHERE ExhibitionName=@ExhibitionName", connection);
			select.Parameters.Add(new SqlParameter("ExhibitionName", ExhibitionName));
			int rows_deleted = select.ExecuteNonQuery();
			connection.Close();
			Console.WriteLine(rows_deleted + " Exhibition deleted");
		}
			return RedirectToPage("/Exhibitions");
	}
}