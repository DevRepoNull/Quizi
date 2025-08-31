using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.UserManagement
{
    [Authorize]
    public class EditUserModel(IUserInterfaces user, IRoleInterfaces role) : PageModel
    {
        //User interfaces primary constructor
        private readonly IUserInterfaces _user = user;

        //Role interfaces primary constructor
        private readonly IRoleInterfaces _role = role;

        [BindProperty]
        public EditUserFromAdminPanelViewModel EditUser { get; set; }
        public async Task<IActionResult> OnGet(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return NotFound();

            EditUser = await _user.GetUserForEditFromAdminPanelAsync(userId);

            if (EditUser == null)
                return NotFound();

            ViewData["RoleData"] = await _role.GetRoleSelectListByIdAsync(EditUser.FkRoleId);
            return Page();
        }

        public async Task<IActionResult> OnPost(string userId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["RoleData"] = await _role.GetRoleSelectListByIdAsync(EditUser.FkRoleId);
                return Page();
            }

            var result = await _user.EditUserFromAdminPanelAsync(userId, EditUser);

            if (result)
            {
                TempData["SuccessMessage"] = "??????? ????? ?? ?????? ?????? ??";
                return RedirectToPage("/UserManagement/IndexUser");
            }

            TempData["ErrorMessage"] = "??? ?? ?????? ?????";
            ViewData["RoleData"] = await _role.GetRoleSelectListByIdAsync(EditUser.FkRoleId);
            return Page();
        }
    }
}
