using Questioning_Data_Repositories.StaticValue;
using System.Diagnostics.CodeAnalysis;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class ExamDataViewModel
    {
        [AllowNull]
        public string FormId { get; set; }

        [Display(Name = "عنوان فرم")]
        public string FormTitle { get; set; }

        [Display(Name = "تاریخ شروع")]
        public string StartExamDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        public string EndExamDate { get; set; }

        [Display(Name = "زمان شروع")]
        public string StartExamTime { get; set; }

        [Display(Name = "زمان پایان")]
        public string EndExamTime { get; set; }
    }

    public class UserAnswerForExam
    {
        public string FormId { get; set; }
        public string FkUserId { get; set; }
        public string QuestionId { get; set; }
        public string AnswerId { get; set; }

        public string SelectedOption { get; set; } // تستی
        public bool? SelectedTrueOrFalse { get; set; } // صحیح/غلط
        public string DescriptiveAnswerText { get; set; } // تشریحی
    }


    public class ExamUserViewModel
    {
        public string FormId { get; set; }

        public string FormTitle { get; set; }
        public List<QuestionAndAnswerForUserExamViewModel> Questions { get; set; }
    }

    public class QuestionAndAnswerForUserExamViewModel
    {
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypeEnumTypes QuestionType { get; set; }
        public List<AnswerUserExamViewModel> Answers { get; set; }
    }

    public class AnswerUserExamViewModel
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

    public class ExamAnswerSheetViewModel
    {
        public string FormId { get; set; }

        public string FormTitle { get; set; }

        public List<AnswerSheetQuestionViewModel> Questions { get; set; }
    }

    public class AnswerSheetQuestionViewModel
    {
        public string QuestionText { get; set; }

        public AnswerTypeEnumTypes QuestionType { get; set; }

        public UserAnswerForExam UserSelectedAnswer { get; set; }

        public CorrectAnswerUserExamViewModel CorrectAnswer { get; set; }
    }

    public class CorrectAnswerUserExamViewModel
    {
        public string AnswerId { get; set; }
        public string AnswerText { get; set; }

        public string QuestionId { get; set; }

        public string FkFormId { get; set; }

        public AnswerTypeEnumTypes AnswerType { get; set; }

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
