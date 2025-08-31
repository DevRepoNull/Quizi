namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class Form : BaseEntity
    {
        #region Default Properties

        public string FormId { get; set; }

        public string FormTitle { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public bool IsPublic { get; set; }

        public string AccessCode { get; set; }

        public TimeOnly StartExamTime { get; set; }

        public TimeOnly EndExamTime { get; set; }

        public DateOnly StartExamDate { get; set; }

        public DateOnly EndExamDate { get; set; }

        #endregion

        #region Relational Properties

        #region User Table

        public string FkUserId { get; set; }

        public User User { get; set; }

        #endregion

        #region Category Table

        public ulong FKCategoryId { get; set; }

        public Categories Categories { get; set; }

        #endregion

        public ICollection<RFormQuestion> RFormQuestions { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }

        #endregion
    }
}
