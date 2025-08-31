using Questioning_Data_Repositories.StaticValue;

namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class Answer : BaseEntity
    {
        #region Default Properties

        public string AnswerId { get; set; }

        public AnswerTypeEnumTypes AnswerTypes { get; set; }

        //For Test Answer
        public string TestOptionOne { get; set; }

        public string TestOptionTwo { get; set; }

        public string TestOptionThree { get; set; }

        public string TestOptionFour { get; set; }

        public string TestCorrectOption { get; set; }

        //For True Or False Answer
        public bool TruOrFalseAnswer { get; set; }

        //For Anatomical Answer
        public string AnswerText { get; set; }

        #endregion

        #region Relational Properties

        #region Question Table

        public string FkQuestionId { get; set; }

        public Question Question { get; set; }

        #endregion

        public ICollection<UserAnswer> UserAnswers { get; set; }

        #endregion
    }
}
