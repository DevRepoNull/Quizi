using Questioning_Data_Repositories.Repositories.Interfaces;
using Questioning_Data_Repositories.Utitlites.Security;
using Questioning_Data_Repositories.ViewModel.Account;
using System.Text.RegularExpressions;

namespace Questioning_Data_Repositories.Repositories.Services
{
    public class UserServices(WebContext web, IGeneralInterfaces general) : IUserInterfaces
    {
        #region User Services Dependency

        private readonly WebContext _web = web;

        private readonly IGeneralInterfaces _general = general;

        #endregion

        #region Crud Admin-Panel Methods

        public async Task<List<UserListFromAdminPanelViewModel>> GetUserListFromAdminPanelAsync()
        {
            return await _web.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .Where(u => !u.IsDelete && u.FkRoleId == u.Role.RoleId)
                .Select(u => new UserListFromAdminPanelViewModel()
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    EmailAddress = u.Email,
                    NationalCode = u.NationalCode,
                    PhoneNumber = u.PhoneNumber,
                    ProfileImageName = u.UserProfile,
                    IsDelete = u.IsDelete,
                    IsActive = u.IsActive,
                    RolTitle = u.Role.RoleName
                }).ToListAsync();
        }

        public async Task<string> AddUserFromAdminPanelAsync(CreateUserFromAdminPanelViewModel model)
        {
            if (model == null) return null;

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = FixedText.FixedEmail(model.EmailAddress),
                NationalCode = model.NationalCode,
                PhoneNumber = model.PhoneNumber,
                Password = await PasswordHasherScripts.EncodePasswordSha256Async(model.Password),
                IsActive = model.IsActive,
                FkRoleId = model.FkRoleId,
                CreateDate = DateTime.UtcNow,
                UpdateDate = null,
                IsDelete = false
            };

            #region Add Images

            if (model.UserProfileUpload != null && await model.UserProfileUpload.IsImageAsync())
            {
                string newUserProfile = await _general.UploadImageFileAsync(model.UserProfileUpload.OpenReadStream(),
                    model.UserProfileUpload.FileName, "/WebsiteImage/UserProfileImage");
                user.UserProfile = newUserProfile;
            }
            else
                user.UserProfile = "DefaultImage.png";

            #endregion

            await _web.Users.AddAsync(user);
            await _web.SaveChangesAsync();

            return user.UserId;
        }

        public async Task<EditUserFromAdminPanelViewModel> GetUserForEditFromAdminPanelAsync(string userId)
        {
            var user = await GetUserByUserIdAsync(userId);

            if (user == null) return null;

            return new EditUserFromAdminPanelViewModel
            {
                UserId = userId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                EmailAddress = user.Email,
                NationalCode = user.NationalCode,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                UserProfileName = user.UserProfile,
                IsActive = user.IsActive,
                FkRoleId = user.FkRoleId
            };
        }

        public async Task<bool> EditUserFromAdminPanelAsync(string userId, EditUserFromAdminPanelViewModel model)
        {
            var user = await GetUserByUserIdAsync(userId);
            if (user == null) return false;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.Email = FixedText.FixedEmail(model.EmailAddress);
            user.NationalCode = model.NationalCode;
            user.PhoneNumber = model.PhoneNumber;

            // Password Check Have Any Text On Update
            if (!string.IsNullOrWhiteSpace(model.Password))
                user.Password = await PasswordHasherScripts.EncodePasswordSha256Async(model.Password);

            user.IsActive = model.IsActive;
            user.FkRoleId = model.FkRoleId;
            user.UpdateDate = DateTime.UtcNow;

            if (model.UserProfileUpload != null && await model.UserProfileUpload.IsImageAsync())
            {
                #region Delete Image

                if (!string.IsNullOrWhiteSpace(user.UserProfile) && user.UserProfile != "DefaultImage.png")
                    await _general.DeleteImageFileAsync("/WebsiteImage/UserProfileImage", user.UserProfile);

                #endregion

                #region Add Images

                string newUserProfile = await _general.UploadImageFileAsync(model.UserProfileUpload.OpenReadStream(),
                    model.UserProfileUpload.FileName, "/WebsiteImage/UserProfileImage");
                user.UserProfile = newUserProfile;

                #endregion
            }

            _web.Users.Update(user);
            await _web.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserFromAdminPanelAsync(string userId)
        {
            var user = await GetUserByUserIdAsync(userId);
            if (user == null) return false;
                
            _web.Users.Remove(user);
            await _web.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SoftDeleteUserFromAdminPanelAsync(string userId)
        {
            var user = await GetUserByUserIdAsync(userId);

            if (user != null)
            {
                user.IsDelete = true;
                user.UpdateDate = DateTime.Now;

                _web.Update(user);
                await _web.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion

        #region User Authentication And Validate Parameters Methods

        public async Task<User> LoginUserFromDbAsync(LoginViewModel loginModel)
        {
            if (loginModel == null ||
                string.IsNullOrWhiteSpace(loginModel.UserOrEmail) ||
                string.IsNullOrWhiteSpace(loginModel.Password))
                return null;

            string emailFixed = FixedText.FixedEmail(loginModel.UserOrEmail);
            string userName = loginModel.UserOrEmail.Trim();

            var userFinder = await _web.Users
                .SingleOrDefaultAsync(u => u.Email == emailFixed || u.UserName == userName);

            if (userFinder == null) return null;

            //bool isPasswordValid = PasswordHasherScripts.VerifyPassword(userFinder.Password, loginModel.Password);
            bool isPasswordValid = await userFinder.Password.VerifyPasswordAsync(loginModel.Password);
            if (!isPasswordValid) return null;

            return userFinder;
        }



        public async Task<string> AddUserOnSignInMethodAsync(SignInViewModel signInModel)
        {
            #region User role attributor

            string defaultRoleTitle = "Client";
            if (!await _web.Users.AnyAsync())
                defaultRoleTitle = "Admin";

            // گرفتن RoleId از دیتابیس (اگر پیدا نشد، مقدار پیش‌فرض User را بده)
            var role = await _web.Roles
                .FirstOrDefaultAsync(r => r.RoleName == defaultRoleTitle);

            if (role == null)
            {
                throw new Exception("Role not found in the database!");
            }

            #endregion

            var user = new User()
            {
                FirstName = signInModel.FirstName,
                LastName = signInModel.LastName,
                Email = FixedText.FixedEmail(signInModel.Email),
                UserName = signInModel.UserName,
                Password = await PasswordHasherScripts.EncodePasswordSha256Async(signInModel.Password),
                PhoneNumber = signInModel.PhoneNumber,
                NationalCode = signInModel.NationalCode,
                UserProfile = "DefaultImage.png",
                FkRoleId = role.RoleId,
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = null
            };

            await _web.AddAsync(user);
            await _web.SaveChangesAsync();

            return user.UserId;
        }

        #endregion

        #region User Finder By Parameters Methods

        public async Task<User> GetUserByUserIdAsync(string userId) =>
            await _web.Users.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == userId);

        public async Task<User> GetUserByEmailAddressAsync(string emailAddress) =>
            await _web.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Email == emailAddress);

        public async Task<User> GetUserByUserNameAsync(string userName) =>
            await _web.Users.AsNoTracking().SingleOrDefaultAsync(u => u.UserName == userName);

        public async Task<string> GetUserIdByUserNameAsync(string userName)
        {
            var userFinder = await _web.Users.AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == userName);

            if (userFinder == null)
                throw new Exception("User not found."); // یا مقدار پیش‌فرض

            return userFinder.UserId;
        }

        #endregion

        #region User Validation Methods

        public async Task<bool> IsExistUserCheckerAsync(string userName) =>
            await _web.Users.AnyAsync(u => u.UserName == userName);

        public async Task<bool> IsExistEmailAddressCheckerAsync(string emailAddress) =>
            await _web.Users.AnyAsync(u => u.Email == emailAddress);

        public ErrorValidationResultsViewModel PasswordValidation(string passwordChecker)
        {
            if ((passwordChecker != null) && !Regex.IsMatch(passwordChecker, @"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$"))
            {
                return new ErrorValidationResultsViewModel()
                {
                    IsValid = false,
                    ErrorMessage = "رمز عبور باید حداقل 8 کاراکتر باشد و شامل حداقل یک حرف و یک عدد باشد"
                };
            }

            return new ErrorValidationResultsViewModel() { IsValid = true };
        }

        public ErrorValidationResultsViewModel EmailAddressValidation(string emailAddressChecker)
        {
            if ((emailAddressChecker != null) && !Regex.IsMatch(emailAddressChecker,
                    @"^(?=.*[A-Za-z])(?=.*[@$!%*?&\\^£`\\\-_\=])(?=.*\d).+$"))
            {
                return new ErrorValidationResultsViewModel()
                {
                    IsValid = false,
                    ErrorMessage = "فقط ایمیل‌های با پسوندهای gmail.com، yahoo.com و outlook.com معتبر هستند."
                };
            }

            return new ErrorValidationResultsViewModel() { IsValid = true };
        }

        #endregion
    }
}
