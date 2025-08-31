using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.RoleManagement
{
    [Authorize]
    public class IndexRoleModel(IRoleInterfaces role) : PageModel
    {
        //Role interface primary constructor
        private IRoleInterfaces _role = role;

        public List<RoleListFromAdminPanelViewModel> RoleList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            RoleList = await _role.GetAllRoleListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
                return BadRequest();

            bool result = await _role.DeleteRoleFromAdminPanelAsync(roleId);

            if (result)
                TempData["SuccessMessage"] = "نقش با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = "مشکلی در حذف نقش به وجود آمد";

            return RedirectToPage();
        }
    }
}
