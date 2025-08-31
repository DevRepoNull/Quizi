namespace Questioning_Data_Repositories.DataTableEF.User_Permission_TBL
{
    public class Role : BaseEntity
    {
        #region Default Properties

        public string RoleId { get; set; }

        public string RoleName { get; set; }

        public string RoleDescription { get; set; }

        public bool RoleActive { get; set; }

        #endregion

        #region Relational Properties

        public ICollection<User> Users { get; set; }

        #endregion
    }
}
