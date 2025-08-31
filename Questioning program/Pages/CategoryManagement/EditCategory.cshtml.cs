using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.CategoryManagement
{
    [Authorize]
    public class EditCategoryModel(IGeneralInterfaces general) : PageModel
    {
        //Category interface primary constructor
        private IGeneralInterfaces _general = general;

        [BindProperty]
        public CategoryViewModel Category { get; set; }
        public async Task<IActionResult> OnGet(ulong categoryId)
        {
            if (categoryId == 0)
                return NotFound();

            Category = await _general.GetAllDataCategoriesBeforeEditAsync(categoryId);

            if (Category == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPost(ulong categoryId)
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _general.UpdateCategoryFromAdminPanelAsync(Category, categoryId);

            return RedirectToPage("/CategoryManagement/IndexCategory");

        }
    }
}
