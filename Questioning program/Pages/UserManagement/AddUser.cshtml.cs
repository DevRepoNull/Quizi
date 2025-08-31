using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.UserManagement
{
    [Authorize]
    public class AddUserModel(IUserInterfaces user, IRoleInterfaces role) : PageModel
    {
        // User interface primary constructor
        private readonly IUserInterfaces _user = user;

        // Role interface primary constructor
        private IRoleInterfaces _roleData = role;

        [BindProperty]
        public CreateUserFromAdminPanelViewModel CreateUser { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ViewData["RoleData"] = await _roleData.GetRoleSelectListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var data = await _user.AddUserFromAdminPanelAsync(CreateUser);
                return RedirectToPage("/UserManagement/IndexUser");
            }

            ViewData["RoleData"] = await _roleData.GetRoleSelectListAsync();
            return Page();
        }
    }
}
