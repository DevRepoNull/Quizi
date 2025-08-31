using Microsoft.AspNetCore.Mvc.Rendering;

namespace Questioning_Data_Repositories.Repositories.Interfaces
{
    public interface IGeneralInterfaces
    {
        #region Image Methods

        Task<bool> DeleteImageFileAsync(string folderPath, string fileName);

        Task<string> UploadImageFileAsync(Stream fileStream, string originalFileName, string folderPath);

        #endregion

        #region Crud Category Methods

        Task<Categories> FindCategoryByCategoryIdAsync(ulong categoryId);

        Task<List<CategoryViewModel>> GetAllCategoriesToListAsync();

        Task<bool> CreateCategoryFromAdminPanelAsync(CategoryViewModel model);

        Task<CategoryViewModel> GetAllDataCategoriesBeforeEditAsync(ulong categoryId);

        Task<bool> UpdateCategoryFromAdminPanelAsync(CategoryViewModel model, ulong categoryId);

        Task<bool> DeleteCategoryFromAdminPanelAsync(ulong categoryId);

        #endregion

        #region Category Selection Methods

        Task<SelectList> GetCategorySelectListAsync();

        Task<SelectList> GetCategorySelectListByCategoryIdAsync(ulong categoryId);

        #endregion
    }
}
