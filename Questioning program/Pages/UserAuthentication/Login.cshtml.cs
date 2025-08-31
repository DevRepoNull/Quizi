using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.Account;
using System.Security.Claims;

namespace Questioning_program.Pages.UserAuthentication
{
    public class LoginModel(IUserInterfaces user) : PageModel
    {
        //Login Interfaces Primary Constructor
        private readonly IUserInterfaces _user = user;

        [BindProperty]
        public LoginViewModel LoginUser { get; set; }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPostAsync(string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var userFinder = await _user.LoginUserFromDbAsync(LoginUser);

                if (userFinder != null)
                {
                    //User Claims
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, userFinder.UserId.ToString()),
                        new Claim(ClaimTypes.Name, userFinder.UserName),
                        new Claim("FirstName", userFinder.FirstName ?? ""),
                        new Claim("LastName", userFinder.LastName ?? ""),
                        new Claim("ProfileImage", userFinder.UserProfile ?? "DefaultImage.png")
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties
                    {
                        IsPersistent = LoginUser.RememberMe
                    };
                    await HttpContext.SignInAsync(principal, properties);

                    //URL Login User
                    ViewData["IsSuccess"] = true;

                    //Redirecting to the return url if it's not empty and not equal
                    if ((!string.IsNullOrWhiteSpace(returnUrl)) &&
                        (returnUrl != "/"))
                    {
                        // Check if the returnUrl is a local URL to prevent open redirect vulnerability
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                    }

                    return Redirect("/");
                }

                //ModelState.AddModelError(nameof(LoginUser.UserOrEmail), "حساب کاربری شما یافت نشد لطفاً مجدداً تلاش کنید");
                ModelState.AddModelError("LoginUser.UserOrEmail", "حساب کاربری شما یافت نشد لطفاً مجدداً تلاش کنید");
                return Page();
            }

            return Page();
        }
    }
}
