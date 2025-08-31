using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.Q_AManagement
{
    [Authorize]
    public class QuestionManagementListModel(IQuestionAndAnswerInterfaces questionAndAnswer) : PageModel
    {
        //Question and answer interfaces primary constructor
        private IQuestionAndAnswerInterfaces _questionAndAnswer = questionAndAnswer;

        public List<QuestionListFromAdminPanelViewModel> QuestionList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            QuestionList = await _questionAndAnswer.GettAllQuestionsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string questionId)
        {
            if (string.IsNullOrWhiteSpace(questionId))
                return BadRequest();

            var result = await _questionAndAnswer.DeleteQuestionAsync(questionId);

            if (result)
                TempData["SuccessMessage"] = "سوال با موفقیت حذف شد";
            else
                TempData["ErrorMessage"] = "مشکلی در حذف سوال به وجود آمد";

            return RedirectToPage();
        }
    }
}
