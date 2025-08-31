using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;
using System.Security.Claims;

namespace Questioning_program.Pages.EnteringTheExam
{
    public class UserExamModel(IFormInterfaces form, IUserInterfaces user) : PageModel
    {
        //Form interfaces primary constructor
        private readonly IFormInterfaces _form = form;

        private readonly IUserInterfaces _user = user;

        [BindProperty]
        public ExamUserViewModel ExamData { get; set; }

        [BindProperty] public List<UserAnswerForExam> UserAnswers { get; set; }

        public string CurrentUserId { get; set; }

        public async Task<IActionResult> OnGetAsync(string examId)
        {
            if (string.IsNullOrEmpty(examId)) return NotFound();

            CurrentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // ExamData ??= new ExamUserViewModel();
            ExamData = await _form.GetExamUserAsync(examId);
            if (ExamData == null) return NotFound();

            // مقداردهی اولیه به لیست پاسخ‌ها برای Bind کردن Razor
            UserAnswers = ExamData.Questions.Select(q => new UserAnswerForExam
            {
                FormId = ExamData.FormId,
                QuestionId = q.QuestionId,
                AnswerId = q.Answers?.FirstOrDefault()?.AnswerId,
                FkUserId = CurrentUserId
            }).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (UserAnswers == null || !UserAnswers.Any())
            {
                ModelState.AddModelError(string.Empty, "هیچ پاسخی ثبت نشده است.");
                return Page();
            }

            await _form.SaveUserAnswerAsync(UserAnswers);
            return Redirect("/EnteringTheExam/ExamListIndex");
        }
    }
}
