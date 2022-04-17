using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace dt_team2.Pages{}

public class LoginModel : PageModel
{
    [BindProperty]
    public Credential Credential { get; set;}
    public void OnGet()
    {
        
    }
}
public class Credential
    {
        [Required]
        public string UserName { get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set;}
    }
