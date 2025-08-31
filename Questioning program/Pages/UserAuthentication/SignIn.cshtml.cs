using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.Utitlites.GeneralTools;
using Questioning_Data_Repositories.ViewModel.Account;

namespace Questioning_program.Pages.UserAuthentication
{
    public class SignInModel(IUserInterfaces user) : PageModel
    {
        //User Interfaces Primary Constructor
        private readonly IUserInterfaces _user = user;

        [BindProperty]
        public SignInViewModel SignInViewModel { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                #region Error Validation Methods

                //Check User Have A Temporary Email Address
                if ((SignInViewModel.Email != null) &&
                    (await _user.IsExistEmailAddressCheckerAsync(FixedText.FixedEmail(SignInViewModel.Email))))
                {
                    ModelState.AddModelError(nameof(SignInViewModel.Email), "این ایمیل قبلا ثبت شده است لطفاً برای ثبت نام از ایمیل دیگری استفاده کنید");
                    return Page();
                }

                //Check User Have A Temporary UserName
                if ((SignInViewModel.UserName != null) &&
                    (await _user.IsExistUserCheckerAsync(SignInViewModel.UserName)))
                {
                    ModelState.AddModelError(nameof(SignInViewModel.UserName), "این نام کاربری قبلا ثبت شده است لطفاً برای ثبت نام از نام کاربری جدید استفاده کنید");
                    return Page();
                }

                //Forcing The User To Use A Strong Password
                var validateUserPassword = _user.PasswordValidation(SignInViewModel.Password);
                if (!validateUserPassword.IsValid)
                {
                    ModelState.AddModelError(nameof(SignInViewModel.Password), validateUserPassword.ErrorMessage);
                    return Page();
                }

                //Forcing The User To User A Validate Email Address
                //Forcing The User To User A Validate Email Address
                if (!string.IsNullOrEmpty(SignInViewModel.Email))
                {
                    var validateEmailAddress = _user.EmailAddressValidation(FixedText.FixedEmail(SignInViewModel.Email));
                    if (!validateEmailAddress.IsValid)
                    {
                        ModelState.AddModelError(nameof(SignInViewModel.Email), validateEmailAddress.ErrorMessage);
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(SignInViewModel.Email), "لطفاً ایمیل را وارد کنید.");
                    return Page();
                }


                #endregion

                return Page();
            }

            // If model is valid, create user
            var userId = await _user.AddUserOnSignInMethodAsync(SignInViewModel);

            // Redirect or handle successful registration
            TempData["SuccessMessage"] = "ثبت نام با موفقیت انجام شد.";
            return Redirect("/Login");
        }
    }
}
