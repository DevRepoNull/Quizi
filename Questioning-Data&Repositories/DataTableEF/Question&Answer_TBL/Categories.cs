namespace Questioning_Data_Repositories.DataTableEF.Question_Answer_TBL
{
    public class Categories : BaseEntity
    {
        #region Default Properties

        public ulong CategoryId { get; set; }

        public string CategoryName { get; set; }

        #endregion

        #region Relational Properties

        public ICollection<Form> Forms { get; set; }

        #endregion
    }
}
