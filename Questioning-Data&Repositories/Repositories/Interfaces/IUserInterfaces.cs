using Questioning_Data_Repositories.ViewModel.Account;

namespace Questioning_Data_Repositories.Repositories.Interfaces
{
    public interface IUserInterfaces
    {
        #region Crud Admin-Panel Methods

        Task<List<UserListFromAdminPanelViewModel>> GetUserListFromAdminPanelAsync();

        Task<string> AddUserFromAdminPanelAsync(CreateUserFromAdminPanelViewModel model);

        Task<EditUserFromAdminPanelViewModel> GetUserForEditFromAdminPanelAsync(string userId);

        Task<bool> EditUserFromAdminPanelAsync(string userId, EditUserFromAdminPanelViewModel model);

        Task<bool> DeleteUserFromAdminPanelAsync(string userId);

        Task<bool> SoftDeleteUserFromAdminPanelAsync(string userId);

        #endregion

        #region User Authentication Methods

        Task<User> LoginUserFromDbAsync(LoginViewModel loginModel);

        Task<string> AddUserOnSignInMethodAsync(SignInViewModel signInModel);

        #endregion

        #region User Finder By Parameters Methods

        Task<User> GetUserByUserIdAsync(string userId);

        Task<User> GetUserByEmailAddressAsync(string emailAddress);

        Task<User> GetUserByUserNameAsync(string userName);

        Task<string> GetUserIdByUserNameAsync(string userName);

        #endregion

        #region User Validation Methods

        Task<bool> IsExistUserCheckerAsync (string userName);

        Task<bool> IsExistEmailAddressCheckerAsync(string emailAddress);

        ErrorValidationResultsViewModel PasswordValidation(string passwordChecker);

        ErrorValidationResultsViewModel EmailAddressValidation(string emailAddressChecker);

        #endregion
    }
}
