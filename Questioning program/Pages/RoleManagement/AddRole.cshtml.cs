using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.RoleManagement
{
    //[Authorize(Roles = "Admin, SuperAdmin")]
    [Authorize]
    public class AddRoleModel(IRoleInterfaces role) : PageModel
    {
        //Role interface primary constructor
        private readonly IRoleInterfaces _role = role;

        [BindProperty]
        public AddRoleFromAdminPanelViewModel AddRole { get; set; }

        public void OnGet()
        { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return Page();

            var result = await _role.AddRoleFromAdminPanelAsync(AddRole);

            if (result != null)
                return RedirectToPage("/RoleManagement/IndexRole");
            else
            {
                ModelState.AddModelError(nameof(AddRole.RoleName), "خطا در ذخیره دیتابیس");
                return Page();
            }
        }
    }
}
