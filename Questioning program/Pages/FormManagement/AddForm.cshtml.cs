using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.ViewModel.AdminPanelVms;

namespace Questioning_program.Pages.FormManagement
{
    [Authorize]
    public class AddFormModel(IFormInterfaces form, IGeneralInterfaces general) : PageModel
    {
        //From interfaces primary constructor
        private readonly IFormInterfaces _form = form;

        private readonly IGeneralInterfaces _general = general;

        [BindProperty]
        public CreateFormExamViewModel CreateForm { get; set; }

        public async Task<IActionResult> OnGet()
        {
            CreateForm ??= new CreateFormExamViewModel();

            //if (CreateForm == null)
            //    CreateForm = new CreateFormExamViewModel();

            // مقداردهی سوالات برای فرم
            CreateForm.Questions = await _form.GetAllQuestionAsync();

            // مقداردهی دسته بندی ها برای ویو
            ViewData["CategoryData"] = await _general.GetCategorySelectListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                // اگر اعتبارسنجی موفق نبود، سوالات و دسته بندی‌ها رو مجدداً بارگذاری کنیم
                CreateForm.Questions = await _form.GetAllQuestionAsync();
                ViewData["CategoryData"] = await _general.GetCategorySelectListAsync();
                return Page();
            }

            // ایجاد فرم و ذخیره در دیتابیس
            var formId = await _form.CreateFormAsync(CreateForm);

            // ریدایرکت به صفحه لیست فرم‌ها
            return RedirectToPage("/FormManagement/IndexForm");
        }
    }
}
