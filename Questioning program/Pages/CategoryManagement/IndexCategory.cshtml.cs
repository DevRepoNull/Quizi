using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.CategoryManagement
{
    [Authorize]
    public class IndexCategoryModel(IGeneralInterfaces general) : PageModel
    {
        //Category interface primary constructor
        private IGeneralInterfaces _general = general;

        public List<CategoryViewModel> Category { get; set; }

        public async Task<IActionResult> OnGet()
        {
            Category = await _general.GetAllCategoriesToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(ulong categoryId)
        {
            if (categoryId == 0)
                return BadRequest();

            var result = await _general.DeleteCategoryFromAdminPanelAsync(categoryId);

            if (result)
                TempData["SuccessMessage"] = "دسته بندی با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = "مشکلی در حذف دسته بندی به وجود آمد";

            return RedirectToPage();
        }
    }
}
