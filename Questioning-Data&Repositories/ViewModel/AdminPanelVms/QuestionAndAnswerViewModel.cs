using System.Diagnostics.CodeAnalysis;
using Questioning_Data_Repositories.StaticValue;

namespace Questioning_Data_Repositories.ViewModel.AdminPanelVms
{
    public class QuestionListFromAdminPanelViewModel
    {
        [AllowNull]
        public string QuestionId { get; set; }

        [Display(Name = "عنوان سوال")]
        public string QuestionText { get; set; }

        [Display(Name = "نوع سوال")]
        public AnswerTypeEnumTypes AnswerType { get; set; }

        public string FkUserId { get; set; }
    }

    public class CreateQuestionFromAdminPanelViewModel
    {
        [Display(Name = "متن سوال")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Unicode]
        public string QuestionText { get; set; }

        [Display(Name = "نوع جواب (صحیح غلط یا تستی)")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public AnswerTypeEnumTypes AnswerType { get; set; }

        //Test answer
        [Display(Name = "جواب اول")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionOne { get; set; }

        [Display(Name = "جواب دوم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionTwo { get; set; }

        [Display(Name = "جواب سوم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionThree { get; set; }

        [Display(Name = "جواب چهارم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionFour { get; set; }

        [Display(Name = "جواب درست")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestCorrectionOption { get; set; }

        //True or false answer
        [Display(Name = "درست و غلط")]
        public bool TrueOrFalseAnswer { get; set; }

        //Anatomical answer
        [Display(Name = "متن جواب")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string AnswerText { get; set; }

        [AllowNull]
        public string FkUserId { get; set; }
    }

    public class EditQuestionFromAdminPanelViewModel
    {
        [AllowNull]
        public string QuestionId { get; set; }

        [Display(Name = "متن سوال")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 5 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Unicode]
        public string QuestionText { get; set; }

        [Display(Name = "نوع جواب (صحیح غلط یا تستی)")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")]
        [Range(1, byte.MaxValue, ErrorMessage = "لطفا {0} را انتخاب کنید")]
        public AnswerTypeEnumTypes AnswerType { get; set; }

        //Test answer
        [Display(Name = "جواب اول")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionOne { get; set; }

        [Display(Name = "جواب دوم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionTwo { get; set; }

        [Display(Name = "جواب سوم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionThree { get; set; }

        [Display(Name = "جواب چهارم")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestOptionFour { get; set; }

        [Display(Name = "جواب درست")]
        [StringLength(150, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 150 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string TestCorrectionOption { get; set; }

        //True or false answer
        [Display(Name = "درست و غلط")]
        public bool TrueOrFalseAnswer { get; set; }

        //Anatomical answer
        [Display(Name = "متن جواب")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "{0} وارد شده نمی تواند کمتر از 1 کاراکتر و بیشتر از 300 کاراکتر باشد")]
        [AllowNull]
        [Unicode]
        public string AnswerText { get; set; }

        [AllowNull]
        public string FkUserId { get; set; }
    }
}
