namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class UserAnswer : BaseEntity
    {
        #region Default And Relational Properties

        public string UAnswerId { get; set; }

        public string DescriptiveAnswerText { get; set; }

        public string SelectedOption { get; set; }

        public bool? SelectedTrueOrFalse { get; set; }

        public DateTime AnswerDate { get; set; }


        //User Property Relation
        public string FkUserId { get; set; }

        public User User { get; set; }


        //Question Property Relation
        public string FkQuestionId { get; set; }

        public Question Question { get; set; }


        //Answer Property Relation
        public string FkAnswerId { get; set; }

        public Answer Answer { get; set; }


        //Form Property Relation
        public string FkFormId { get; set; }

        public Form Form { get; set; }

        #endregion
    }
}
