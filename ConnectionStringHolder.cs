namespace dt_team2;

public class CSHolder
{
    public static string GetConnectionString()
    {
        return "Server=tcp:dt-team2.database.windows.net,1433;Initial Catalog=dt-team2;Persist Security Info=False;User ID=team2admin;Password=COSC3380#;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    }
}
