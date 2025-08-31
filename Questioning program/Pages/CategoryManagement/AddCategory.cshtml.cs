using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.CategoryManagement
{
    [Authorize]
    public class AddCategoryModel(IGeneralInterfaces general) : PageModel
    {
        //Category interface primary constructor
        private readonly IGeneralInterfaces _general = general;

        [BindProperty]
        public CategoryViewModel Category { get; set; }
        public void OnGet()
        { }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _general.CreateCategoryFromAdminPanelAsync(Category);

            return RedirectToPage("/CategoryManagement/IndexCategory");
        }
    }
}
