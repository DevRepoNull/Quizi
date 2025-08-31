namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class RFormQuestion
    {
        public string FormQuestionId { get; set; }

        public int Order { get; set; }

        #region Form Tabel

        public string FkFormId { get; set; }

        public Form Form { get; set; }

        #endregion

        #region Question Table

        public string FkQuestionId { get; set; }

        public Question Question { get; set; }

        #endregion
    }
}
