using Microsoft.AspNetCore.Mvc.Rendering;

namespace Questioning_Data_Repositories.Repositories.Interfaces
{
    public interface IRoleInterfaces
    {
        #region Crud Role Management From Admin Panel

        Task<string> AddRoleFromAdminPanelAsync(AddRoleFromAdminPanelViewModel roleModel);

        Task<Role> GetRoleByIdAsync(string roleId);

        Task<List<RoleListFromAdminPanelViewModel>> GetAllRoleListAsync();

        Task<EditRoleFromAdminPanel> ShowRoleDataBeforeEditFromAdminPanelAsync(string roleId);

        Task<bool> UpdateRoleFromAdminPanelAsync(string roleId, EditRoleFromAdminPanel roleModel);

        Task<bool> DeleteRoleFromAdminPanelAsync(string roleId);

        #endregion

        #region Role Selection Methods

        Task<SelectList> GetRoleSelectListAsync();

        Task<SelectList> GetRoleSelectListByIdAsync(string roleId);

        #endregion
    }
}
