using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.FormManagement
{
    [Authorize]
    public class FormManagementListModel(IFormInterfaces form) : PageModel
    {
        //Form Interface Primary Constructor
        private readonly IFormInterfaces _form = form;

        [BindProperty]
        public List<FormExamListViewModel> FormExamList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FormExamList = await _form.GetAllFormExamAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string formId)
        {
            if (string.IsNullOrWhiteSpace(formId))
                return BadRequest();

            bool result = await _form.DeleteFormAsync(formId); // حذف واقعی فرم

            if (result)
                TempData["SuccessMessage"] = "فرم با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = "مشکلی در حذف فرم به وجود آمد";

            return RedirectToPage();
        }
    }
}
