using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class ArtPiece
{

    public void OnGet() {}
    public string OrigPieceName { get; set; } = default!;
    //Art Piece Enitity Attributes
    public string title { get; set; } = default!;
    public string creator { get; set; } = default!;
    public string desc { get; set; } = default!;
    public string dim { get; set; } = default!;
    public string origin { get; set; } = default!;
    public DateTime makedate { get; set; } = default!;
    public DateTime getdate { get; set; } = default!;
    public string source { get; set; } = default!;
    public int medium { get; set; } = default!;
}

