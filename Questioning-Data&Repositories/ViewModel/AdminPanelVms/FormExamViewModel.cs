using System.Diagnostics.CodeAnalysis;
using Questioning_Data_Repositories.StaticValue;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class FormExamListViewModel
    {
        [AllowNull]
        public string FormId { get; set; }

        [Display(Name = "عنوان فرم")]
        public string FormTitle { get; set; }

        [Display(Name = "توضیحات فرم")]
        public string FormDescription { get; set; }

        [Display(Name = "وضعیت فرم")]
        public bool IsActive { get; set; }

        [Display(Name = "کاربران عمومی")]
        public bool IsPublic { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        public string StartExamTime { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        public string EndExamTime { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        public string StartExamDate { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        public string EndExamDate { get; set; }

        [Display(Name = "دسته بندی")]
        public string CategoryTitle { get; set; }
    }

    public class CreateFormExamViewModel
    {
        //Form Property Attributes

        [Display(Name = "عنوان فرم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 100 کاراکتر باشد")]
        [Unicode]
        public string FormTitle { get; set; }

        [Display(Name = "توضیحات فرم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [Unicode]
        public string FormDescription { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "عمومی بودن")]
        public bool IsPublic { get; set; }

        [Display(Name = "کد شناسایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AccessCode { get; set; }

        [Display(Name = "زمان شروع آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string StartExamTime { get; set; }

        [Display(Name = "زمان پایان آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EndExamTime { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string StartExamDate { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EndExamDate { get; set; }

        //Select List Form Attributes

        [Display(Name = "لیست سوالات")]
        public List<Question> Questions { get; set; }

        public List<string> SelectedQuestions { get; set; }

        //Form Relation Attributes

        [Display(Name = "لیست دسته بندی ها")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public ulong FkCategoryId { get; set; }

        [AllowNull]
        public string FkUserId { get; set; }
    }

    public class EditFormExamViewModel
    {
        //Form Property Attributes

        public string? FormId { get; set; }

        [Display(Name = "عنوان فرم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 100 کاراکتر باشد")]
        [Unicode]
        public string FormTitle { get; set; }

        [Display(Name = "توضیحات فرم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(300, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [Unicode]
        public string FormDescription { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "عمومی بودن")]
        public bool IsPublic { get; set; }

        [Display(Name = "کد شناسایی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string AccessCode { get; set; }

        [Display(Name = "زمان شروع آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string StartExamTime { get; set; }

        [Display(Name = "زمان پایان آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EndExamTime { get; set; }

        [Display(Name = "تاریخ شروع آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string StartExamDate { get; set; }

        [Display(Name = "تاریخ پایان آزمون")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string EndExamDate { get; set; }

        //Select List Form Attributes

        [Display(Name = "لیست سوالات")]
        public List<Question> Questions { get; set; }

        public List<SelectedQuestionViewModel> SelectedQuestions { get; set; }

        //Form Relation Attributes

        [Display(Name = "لیست دسته بندی ها")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public ulong FkCategoryId { get; set; }

        [AllowNull]
        public string FkUserId { get; set; }
    }


    public class SelectedQuestionViewModel
    {
        public string QuestionId { get; set; }

        [Display(Name = "عنوان سوال")]
        public string QuestionTitle { get; set; }

        [Display(Name = "ترتیب")]
        public int Order { get; set; }
    }

    public class ShowExamFormViewModel
    {
        public string FormTitle { get; set; }
        public List<QuestionWithAnswersViewModel> Questions { get; set; }
    }

    public class QuestionWithAnswersViewModel
    {
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypeEnumTypes QuestionType { get; set; }
        public List<AnswerViewModel> Answers { get; set; }
    }

    public class AnswerViewModel
    {
        public string AnswerId { get; set; }
        public string AnswerText { get; set; }

        // برای سوالات تستی
        public string OptionOne { get; set; }
        public string OptionTwo { get; set; }
        public string OptionThree { get; set; }
        public string OptionFour { get; set; }
        public string CorrectOption { get; set; }

        // برای سوالات درست و غلط
        public bool TrueOrFalseAnswer { get; set; }
    }
}
