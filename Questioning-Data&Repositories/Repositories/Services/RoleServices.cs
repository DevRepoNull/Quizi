using Microsoft.AspNetCore.Mvc.Rendering;
using Questioning_Data_Repositories.Repositories.Interfaces;

namespace Questioning_Data_Repositories.Repositories.Services
{
    //This Is Primary Constructor On Dependency Injection
    public class RoleServices(WebContext webContext) : IRoleInterfaces
    {
        #region Role Db Dependency

        private readonly WebContext _webContext = webContext;

        #endregion

        #region Crud Role Management From Admin Panel
        public async Task<string> AddRoleFromAdminPanelAsync(AddRoleFromAdminPanelViewModel roleModel)
        {
            if (roleModel == null)
                throw new ArgumentNullException(nameof(roleModel));

            try
            {
                var role = new Role
                {
                    RoleName = roleModel.RoleName.Trim(),
                    RoleDescription = roleModel.RoleDescription?.Trim(),
                    RoleActive = roleModel.RoleActive,
                    CreateDate = DateTime.UtcNow
                };

                await _webContext.Roles.AddAsync(role);
                await _webContext.SaveChangesAsync();

                return role.RoleId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in AddRoleFromAdminPanelAsync: " + ex.Message, ex);
            }
        }

        public async Task<Role> GetRoleByIdAsync(string roleId) =>
            await _webContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);

        public async Task<List<RoleListFromAdminPanelViewModel>> GetAllRoleListAsync()
        {
            return await _webContext.Roles
                .AsNoTracking()
                .Select(r => new RoleListFromAdminPanelViewModel()
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName,
                    RoleDescription = r.RoleDescription,
                    RolActive = r.RoleActive
                }).ToListAsync();
        }

        public async Task<EditRoleFromAdminPanel> ShowRoleDataBeforeEditFromAdminPanelAsync(string roleId)
        {
            var role = await GetRoleByIdAsync(roleId);
            if (role != null)
            {
                return new EditRoleFromAdminPanel()
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription,
                    RoleActive = role.RoleActive
                };
            }

            return null;
        }

        public async Task<bool> UpdateRoleFromAdminPanelAsync(string roleId, EditRoleFromAdminPanel roleModel)
        {
            var roleData = await GetRoleByIdAsync(roleId);
            if (roleData != null)
            {
                roleData.RoleName = roleModel.RoleName;
                roleData.RoleDescription = roleModel.RoleDescription;
                roleData.RoleActive = roleModel.RoleActive;
                roleData.UpdateDate = DateTime.UtcNow;

                _webContext.Update(roleData);
                await _webContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRoleFromAdminPanelAsync(string roleId)
        {
            var role = await GetRoleByIdAsync(roleId);

            if (role != null)
            {
                _webContext.Remove(role);
                await _webContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        #endregion

        #region Role Selection Methods

        public async Task<SelectList> GetRoleSelectListAsync()
        {
            var roleData = await _webContext.Roles
                .Where(r => r.RoleActive)
                .Select(r => new DataCombineViewModel()
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                }).Distinct().ToListAsync();

            if (roleData == null)
                throw new Exception("Role Data Was Not Found");

            return new SelectList(roleData, nameof(DataCombineViewModel.RoleId), nameof(DataCombineViewModel.RoleName));
        }

        public async Task<SelectList> GetRoleSelectListByIdAsync(string roleId)
        {
            var roleData = await _webContext.Roles
                .Where(r => r.RoleActive)
                .Select(r => new DataCombineViewModel()
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                }).Distinct().ToListAsync();

            if (roleData == null)
                throw new Exception("Role Data Was Not Found");

            return new SelectList(roleData, nameof(DataCombineViewModel.RoleId), nameof(DataCombineViewModel.RoleName),
                roleId);
        }

        #endregion
    }
}
