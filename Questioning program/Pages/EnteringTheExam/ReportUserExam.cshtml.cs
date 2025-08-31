using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.EnteringTheExam
{
    public class ReportUserExamModel(IFormInterfaces exam) : PageModel
    {
        //Form Interfaces Primary Dependency Injection
        private readonly IFormInterfaces _exam = exam;

        [BindProperty]
        public ExamAnswerSheetViewModel AnswerSheet { get; set; }

        public async Task<IActionResult> OnGetAsync(string examId, string userId)
        {
            AnswerSheet = await _exam.GetExamAnswerSheetAsync(examId, userId);

            if (AnswerSheet == null)
                return NotFound();

            return Page();
        }
    }
}
