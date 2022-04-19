using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class EditArtPieceModel : PageModel
{
	private readonly ILogger<EditArtPieceModel> _logger;

	public ArtPiece piece = new ArtPiece();

	public EditArtPieceModel(ILogger<EditArtPieceModel> logger)
	{
		_logger = logger;
	}

	public void OnGet(string id)
	{

		OrigPieceName = id;

		using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString()))
		{
			connection.Open();
			SqlCommand select = new SqlCommand("SELECT Title, Creator, PieceDescription, PlaceOfOrigin, DateCreated, DateSubmitted, Dimensions, Source, Medium FROM dbo.ArtPieces WHERE Title=@OrigPieceName", connection);
			select.Parameters.Add(new SqlParameter("OrigPieceName", OrigPieceName));
			SqlDataReader data = select.ExecuteReader();

			if (data.Read())
			{
				piece = new ArtPiece
				{
					OrigPieceName = data["Title"].ToString()!,
					title = data["Title"].ToString()!,
					creator = data["Creator"].ToString()!,
					// ListOfPieces = data["ListOfPieces"].ToString()!,
					desc = data["PieceDescription"].ToString()!,
					dim = data["Dimensions"].ToString()!,
					origin = data["PlaceOfOrigin"].ToString()!,
					makedate = (DateTime)data["DateCreated"],
					getdate = (DateTime)data["DateSubmitted"],
					source = data["Source"].ToString()!,
					medium = (int)data["Medium"],
				};
			}
			connection.Close();
		}
	}

	public string OrigPieceName
	{
		get; set;
	} = default!;
	public string Title
	{
		get; set;
	} = default!;
	public string Creator
	{
		get; set;
	} = default!;
	public string PieceDescription
	{
		get; set;
	} = default!;
	public string PlaceOfOrigin
	{
		get; set;
	} = default!;
	public DateTime DateCreated
	{
		get; set;
	} = default!;
	public DateTime DateSubmitted
	{
		get; set;
	} = default!;
	public string Dimensions
	{
		get; set;
	} = default!;
	public string Source
	{
		get; set;
	} = default!;
	public int Medium
	{
		get; set;
	} = default!;
	// submit form to OnPost
	public IActionResult OnPost(ArtPiece piece)
	{
		OrigPieceName = piece.OrigPieceName;
		Title = piece.title;
		Creator = piece.creator;
		PieceDescription = piece.desc;
		PlaceOfOrigin = piece.origin;
		DateCreated = piece.makedate;
		DateSubmitted = piece.getdate;
		Dimensions = piece.dim;
		Source = piece.source;
		Medium = piece.medium;
		String[] fields = { OrigPieceName, Title, Creator, PieceDescription, PlaceOfOrigin, DateCreated.ToString(), DateSubmitted.ToString(), Dimensions, Source, Medium.ToString() };
		//if (DateEnd == new DateTime(1, 1, 1)) ModelState.AddModelError("DateEnd", "Please set a date for the exhibition to end!");
		//else if (DateEnd < DateTime.Today) ModelState.AddModelError("DateEnd", "Exhibition must end after today!");
		if ((new String[] { OrigPieceName, Title, Creator, PieceDescription, PlaceOfOrigin, DateCreated.ToString(), DateSubmitted.ToString(), Dimensions, Source, Medium.ToString() }.All(field => !String.IsNullOrEmpty(field))))
		{
			using (SqlConnection connection = new SqlConnection(CSHolder.GetConnectionString()))
			{
				connection.Open();
				SqlCommand select = new SqlCommand("UPDATE dbo.ArtPieces SET Title = @Title, Creator = @Creator, PieceDescription = @PieceDescription, PlaceOfOrigin = @PlaceOfOrigin, DateCreated = @DateCreated, DateSubmitted = @DateSubmitted, Dimensions = @Dimensions, Source = @Source, Medium = @Medium WHERE Title=@OrigPieceName", connection);
				select.Parameters.Add(new SqlParameter("OrigPieceName", OrigPieceName));
				select.Parameters.Add(new SqlParameter("Title", Title));
				select.Parameters.Add(new SqlParameter("Creator", Creator));
				select.Parameters.Add(new SqlParameter("PieceDescription", PieceDescription));
				select.Parameters.Add(new SqlParameter("PlaceOfOrigin", PlaceOfOrigin));
				select.Parameters.Add(new SqlParameter("DateCreated", DateCreated));
				select.Parameters.Add(new SqlParameter("DateSubmitted", DateSubmitted));
				select.Parameters.Add(new SqlParameter("Dimensions", Dimensions));
				select.Parameters.Add(new SqlParameter("Source", Source));
				select.Parameters.Add(new SqlParameter("Medium", Medium));
				int rows_added = select.ExecuteNonQuery();
				connection.Close();
				Console.WriteLine(rows_added + " Art piece edited");
			}
			return RedirectToPage("/ArtPieces");
		}
		return Page();
	}
}
