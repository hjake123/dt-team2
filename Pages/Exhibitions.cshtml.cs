using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dt_team2.Pages;

public class ExhibitionsModel : PageModel 
{
    private readonly ILogger<ExhibitionsModel> _logger;
    public static List<Exhibition> exhibitions = new List<Exhibition>();
    private string c_string = CSHolder.GetConnectionString();

    public ExhibitionsModel(ILogger<ExhibitionsModel> logger) {
        _logger = logger;
    }

    public void GetExhibitions() {
        if (exhibitions.Count > 0) exhibitions.Clear();

        try {
            using (SqlConnection connection = new SqlConnection(c_string)) {
                connection.Open();
                SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Exhibitions]", connection);
                SqlDataReader data = selectCommand.ExecuteReader();
                List<Exhibition> list = new List<Exhibition>();

                while (data.Read()) {
                    list.Add(new Exhibition {
                        ExhibitionName = data["ExhibitionName"].ToString()!,
                        Description = data["Description"].ToString()!,
                        // ListOfPieces = data["ListOfPieces"].ToString()!,
                        Arranger = data["Arranger"].ToString()!,
                        Location = data["Location"].ToString()!,
                        DateEnd = (DateTime)data["DateEnd"],
                    });
                }
                exhibitions = list;

                connection.Close();
            };
        }
        catch (Exception) { throw; }
    }

    public void OnGet() {
        if(Request.Cookies["session_user"] == null){
            // Then no session cookie exists and they're not logged in! Get 'em out of here!
            Response.Redirect("Login");
        }
        GetExhibitions();
    }

    public string Search { get; set; } = default!;
}
