using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.Q_AManagement
{
    [Authorize]
    public class EditQuestionModel(IQuestionAndAnswerInterfaces questionAndAnswer) : PageModel
    {
        //Question interface primary constructor

        private readonly IQuestionAndAnswerInterfaces _questionAndAnswer = questionAndAnswer;

        [BindProperty]
        public EditQuestionFromAdminPanelViewModel EditQuestion { get; set; }

        public async Task<IActionResult> OnGetAsync(string questionId)
        {
            if (string.IsNullOrWhiteSpace(questionId))
                return NotFound();

            EditQuestion = await _questionAndAnswer.GetQuestionForEditAsync(questionId);

            if (EditQuestion == null)
                return NotFound();

            ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeListById(questionId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string questionId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeListById(questionId);
                return Page();
            }

            var result = await _questionAndAnswer.EditQuestionAsync(questionId, EditQuestion);
            if(result)
                return RedirectToPage("/Q&AManagement/QuestionManagementList");

            ModelState.AddModelError(string.Empty, "ویرایش سوال با خطا مواجه شد.");
            ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeList();
            return Page();
        }
    }
}
