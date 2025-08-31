using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.RoleManagement
{
    [Authorize]
    public class EditRoleModel(IRoleInterfaces role) : PageModel
    {
        //Role interface primary constructor
        private readonly IRoleInterfaces _role = role;

        [BindProperty]
        public EditRoleFromAdminPanel EditRole { get; set; }

        public async Task<IActionResult> OnGet(string roleId)
        {
            if (string.IsNullOrWhiteSpace(roleId))
                return NotFound();

            EditRole = await _role.ShowRoleDataBeforeEditFromAdminPanelAsync(roleId);

            if (EditRole == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(string roleId)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _role.UpdateRoleFromAdminPanelAsync(roleId, EditRole);

            if (!result)
                return BadRequest();

            return RedirectToPage("/RoleManagement/IndexRole");
        }
    }
}
