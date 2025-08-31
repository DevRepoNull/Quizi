namespace Questioning_Data_Repositories.DataTableEF.User_Permission_TBL
{
    public class User : BaseEntity
    {
        #region Default Properies

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string NationalCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string UserProfile { get; set; }

        #endregion

        #region Relational Properties

        public string FkRoleId { get; set; }

        public Role Role { get; set; }

        public ICollection<UserAnswer> UserAnswers { get; set; }

        public ICollection<Question> Questions { get; set; }

        public ICollection<Form> Forms { get; set; }

        #endregion
    }
}
