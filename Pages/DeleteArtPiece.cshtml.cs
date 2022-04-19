using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class DeleteArtPieceModel : PageModel
{
	private readonly ILogger<DeleteArtPieceModel> _logger;

	public DeleteArtPieceModel(ILogger<DeleteArtPieceModel> logger)
	{
		_logger = logger;
	}

	public void OnGet(string id)
	{
		Title = id;
		if (Request.Cookies["session_user_role"] != "admin")
		{
			// Only admins can delete items!
			Response.Redirect("ForbiddenAction");
		}
	}

	public string Title
	{
		get; set;
	} = default!;

	// submit form to OnPost
	public IActionResult OnPost(ArtPiece piece)
	{
		Title = piece.title;

		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString()))
		{
			connection.Open();
			SqlCommand select = new SqlCommand("DELETE FROM dbo.ArtPieces WHERE Title=@Title", connection);
			select.Parameters.Add(new SqlParameter("Title", Title));
			int rows_deleted = select.ExecuteNonQuery();
			connection.Close();
			Console.WriteLine(rows_deleted + " Art piece deleted");
		}
		return RedirectToPage("/ArtPieces");
	}
}
