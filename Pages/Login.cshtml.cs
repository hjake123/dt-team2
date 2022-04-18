using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages;

public class loginInfo {
    public string userName { get; set;} = default!;
    public string password { get; set;}= default!;
    
}
    

public class LoginModel : PageModel
{   
    public string userName = default!;
    public string password = default!;

    public void OnPost(loginInfo checkLogin)
    {
        userName = checkLogin.userName;
        password = checkLogin.password;

        string connectionString = CSHolder.GetConnectionString();
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand selectCommand = new SqlCommand("SELECT PASSWORD FROM [dbo].[tbl_login] WHERE USER_NAME =  '" + userName + "' " , conn);
            SqlDataReader results = selectCommand.ExecuteReader();          
            while(results.Read())
            {   
                if(results[0].ToString()==password)
                {
                    Response.Redirect("Index");
                }
        
            }

        }
    }
}
