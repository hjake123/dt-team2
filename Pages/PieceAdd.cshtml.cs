using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;

namespace dt_team2.Pages;

public class PieceEntity{
    //Art Piece Enitity Attributes
    public string title {get; set;} = default!;
    public string creator {get; set;} = default!;
    public string desc {get; set;} = default!;
    public string dim {get; set;} = default!;
    public string origin {get; set;} = default!;
    public DateTime makedate {get; set;} = default!;
    public DateTime getdate {get; set;} = default!;
    public string source {get; set;} = default!;
    public int medium {get; set;} = default!;
    public string collection {get; set;} = default!;
}

public class PageAddModel : PageModel
{
    public int pieceID = default!;
    public string title = default!;
    public string creator = default!;
    public string desc = default!;
    public string dim = default!;
    public string origin = default!;
    public DateTime makedate = default!;
    public DateTime getdate = default!;
    public string source = default!;
    public int medium = default!;
    public string collection = default!;

    public List<SelectListItem> media{get; set;} = new List<SelectListItem>();

    public List<SelectListItem> collections{get; set;} = new List<SelectListItem>();

    private readonly ILogger<IndexModel> _logger;

    public PageAddModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
        media = GetMedia();
        collections = GetCollections();
    }

    private bool DoesIDExist(int id){
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT PieceID FROM dbo.ArtPieces WHERE PieceID = " + id, conn); 
            SqlDataReader results = selectCommand.ExecuteReader();  

            while(results.Read()){
                if(results["PieceID"].ToString() == id.ToString()){
                    return true; 
                }
            }            

            conn.Close();
        }
        return false;
    }
    private int GenerateID(){
        Random rnd = new Random();
        int id = rnd.Next();

        while(DoesIDExist(id)){
            id = rnd.Next();
        }

        return id;
    }
    public void OnPost(PieceEntity ent) {
        string connectionString = CSHolder.GetConnectionString();

        pieceID = GenerateID();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();   
            SqlCommand command = new SqlCommand("INSERT INTO dbo.ArtPieces VALUES (" + pieceID + ", '" + ent.title + "', '" + ent.creator + "' , '" + ent.desc + "' , '" + ent.origin + "' , '" + ent.makedate + "' , '" + ent.getdate + "' , '" + ent.dim + "' , '" + ent.source + "' , '" + ent.medium + "');", conn);      
            try{
                command.ExecuteNonQuery();
            } catch(SqlException e)
            {
                Console.WriteLine(e.ToString());
                Console.WriteLine(command.CommandText);
            }
{}          conn.Close();
        }
    }

    private List<SelectListItem> GetMedia(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        tempAccess.Add(new SelectListItem{Value = "0", Text ="Select Medium"});
        //connect to database
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[LookUp_Medium]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            while(results.Read()){
                tempAccess.Add(new SelectListItem{Value = results["Medium"].ToString(), 
                    Text = results["MediumLabel"].ToString() + " // Medium ID: " + results["Medium"].ToString()});                
            }
            
            conn.Close();
        }
        return tempAccess;
    }

    private List<SelectListItem> GetCollections(){
        List<SelectListItem> tempAccess = new List<SelectListItem>();

        tempAccess.Add(new SelectListItem{Value = "", Text ="Select Collection"});
        //connect to database
        string connectionString = CSHolder.GetConnectionString();

        using(SqlConnection conn = new SqlConnection(connectionString)){
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM [dbo].[Collections]", conn);
            SqlDataReader results = selectCommand.ExecuteReader();                

            int i = 1;
            while(results.Read()){
                tempAccess.Add(new SelectListItem{Value = results["CollectionName"].ToString(), 
                    Text = results["CollectionName"].ToString() + " // Collection ID: " + i.ToString()});                
                i++;
            }
            
            conn.Close();
        }
        return tempAccess;
    }
}
