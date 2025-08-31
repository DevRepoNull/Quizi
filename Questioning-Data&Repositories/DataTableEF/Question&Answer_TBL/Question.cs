namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class Question : BaseEntity
    {
        #region Default Properties

        public string QuestionId { get; set; }

        public string QuestionText { get; set; }

        #endregion

        #region Relational Properties

        #region User Table

        public string FkUserId { get; set; }

        public User User { get; set; }

        #endregion

        public ICollection<Answer> Answers { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }

        public ICollection<RFormQuestion> RFormQuestions { get; set; }

        #endregion
    }
}
