using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.FormManagement
{
    [Authorize]
    public class ShowExamFormModel(IFormInterfaces form) : PageModel
    {
        //Form interfaces primary constructor
        private IFormInterfaces _form = form;

        [BindProperty]
        public ShowExamFormViewModel ExamFormModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string formId)
        {
            ExamFormModel = await _form.GetExamFormByIdAsync(formId);
            if (ExamFormModel == null)
                return NotFound();

            return Page();
        }
    }
}
