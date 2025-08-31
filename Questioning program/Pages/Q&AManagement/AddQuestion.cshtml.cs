using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.Q_AManagement
{
    [Authorize]
    public class AddQuestionModel(IQuestionAndAnswerInterfaces questionAndAnswer) : PageModel
    {
        //Question interface primary constructor

        private readonly IQuestionAndAnswerInterfaces _questionAndAnswer = questionAndAnswer;

        [BindProperty]
        public CreateQuestionFromAdminPanelViewModel CreateQuestion { get; set; }
        public void OnGet()
        {
            ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeList();
                return Page();
            }

            var result = await _questionAndAnswer.CreateQuestionAsync(CreateQuestion);
            //return RedirectToPage("/Q&AManagement/IndexQuestion");

            if (result)
                return RedirectToPage("/Q&AManagement/IndexQuestion");
            else
            {
                ViewData["AnswerData"] = _questionAndAnswer.SelectAnswerTypeList();
                return Page();
            }
        }
    }
}
