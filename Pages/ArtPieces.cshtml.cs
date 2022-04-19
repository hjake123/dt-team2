using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class ArtPiecesModel : PageModel
{
	private readonly ILogger<ArtPiecesModel> _logger;
	public static List<ArtPiece> pieces = new List<ArtPiece>();
	private string c_string = CSHolder.GetConnectionString();

	public ArtPiecesModel(ILogger<ArtPiecesModel> logger)
	{
		_logger = logger;
	}

	public void GetArtPiece()
	{
		if (pieces.Count > 0) pieces.Clear();

		try
		{
			using (SqlConnection connection = new SqlConnection(c_string))
			{
				connection.Open();
				SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[ArtPieces]", connection);
				SqlDataReader data = selectCommand.ExecuteReader();
				List<ArtPiece> list = new List<ArtPiece>();

				while (data.Read())
				{
					list.Add(new ArtPiece
					{
						title = data["Title"].ToString()!,
						creator = data["Creator"].ToString()!,
						desc = data["PieceDescription"].ToString()!,
						dim = data["Dimensions"].ToString()!,
						origin = data["PlaceOfOrigin"].ToString()!,
						makedate = (DateTime)data["DateCreated"],
						getdate = (DateTime)data["DateSubmitted"],
						source = data["Source"].ToString()!,
						medium = (int)data["Medium"],
					});
				}
				pieces = list;

				connection.Close();
			};
		}
		catch (Exception) { throw; }
	}

	public void OnGet()
	{
		if (Request.Cookies["session_user"] == null)
		{
			// Then no session cookie exists and they're not logged in! Get 'em out of here!
			Response.Redirect("Login");
		}
		GetArtPiece();
	}

	public string Search
	{
		get; set;
	} = default!;
}


