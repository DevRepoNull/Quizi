using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.DataTableEF.User_Permission_TBL;
using Questioning_Data_Repositories.Repositories.Interfaces;
using System.Security.Claims;

namespace Questioning_program.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        public void OnGet() { }

        //[HttpGet("/LogOut")]
        public async Task<IActionResult> OnGetLogOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
    }
}
