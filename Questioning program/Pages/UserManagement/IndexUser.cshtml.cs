using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.UserManagement
{
    [Authorize]
    public class UserListModel(IUserInterfaces user) : PageModel
    {
        //UserInterface Primary Polymorphism
        private readonly IUserInterfaces _user = user;

        [BindProperty]
        public List<UserListFromAdminPanelViewModel> UserLists { get; set; }

        public async Task<IActionResult> OnGet()
        {
            UserLists = await _user.GetUserListFromAdminPanelAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            bool result = await _user.SoftDeleteUserFromAdminPanelAsync(userId);

            if (result)
                TempData["SuccessMessage"] = "کاربر با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = "مشکلی در حذف کاربر به وجود آمد";

            return RedirectToPage();
        }
    }
}
