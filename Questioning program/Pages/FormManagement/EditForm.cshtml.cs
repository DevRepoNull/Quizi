using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.FormManagement
{
    [Authorize]
    public class EditFormModel(IFormInterfaces form, IGeneralInterfaces general) : PageModel
    {
        //Edit Form Primary Constructor
        private readonly IFormInterfaces _form = form;

        private readonly IGeneralInterfaces _general = general;

        [BindProperty]
        public EditFormExamViewModel EditForm { get; set; }

        public async Task<IActionResult> OnGet(string formId)
        {
            if (formId != null)
            {
                EditForm = await _form.GetFormBeforeEditAsync(formId);

                if (EditForm == null)
                {
                    return NotFound();
                }

                EditForm.Questions = await _form.GetAllQuestionAsync();

                var selected = await _form.GetQuestionOfRFormAsync(formId);

                EditForm.SelectedQuestions = selected
                    .Select(q => new SelectedQuestionViewModel
                    {
                        QuestionId = q.Question.QuestionId,
                        QuestionTitle = q.Question.QuestionText,
                        Order = q.Order
                    }).ToList();

                ViewData["CategoryData"] = await _general.GetCategorySelectListByCategoryIdAsync(EditForm.FkCategoryId);

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(string formId)
        {
            if (!ModelState.IsValid)
            {
                // در صورت خطا، دوباره دیتاها را لود کن برای بازگشت به View
                EditForm.Questions = await _form.GetAllQuestionAsync();
                ViewData["CategoryData"] = await _general.GetCategorySelectListByCategoryIdAsync(EditForm.FkCategoryId);
                return Page();
            }

            // ثبت تغییرات فرم
            var result = await _form.EditFormAsync(formId, EditForm);

            if (!result)
            {
                // اگر ثبت با خطا مواجه شد، پیام خطا نشان داده شود
                ModelState.AddModelError(string.Empty, "خطا در ویرایش فرم رخ داد. مجدد تلاش کنید.");
                EditForm.Questions = await _form.GetAllQuestionAsync();
                ViewData["CategoryData"] = await _general.GetCategorySelectListByCategoryIdAsync(EditForm.FkCategoryId);
                return Page();
            }

            // ثبت موفقیت آمیز ➔ بازگشت به لیست فرم‌ها یا نمایش پیام موفقیت
            return RedirectToPage("/FormManagement/IndexForm");
        }
    }
}
