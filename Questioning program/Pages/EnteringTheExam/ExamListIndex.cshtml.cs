using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.EnteringTheExam
{
    public class ExamListIndexModel(IFormInterfaces form) : PageModel
    {
        //Form Interfaces Primary Constructor
        private readonly IFormInterfaces _form = form;

        [BindProperty]
        public List<ExamDataViewModel> ExamList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ExamList = await _form.GetExamListAsync();
            return Page();
        }
    }
}
