using Microsoft.AspNetCore.Mvc.Rendering;
using Questioning_Data_Repositories.Repositories.Interfaces;

namespace Questioning_Data_Repositories.Repositories.Services
{
    public class GeneralServices(WebContext context) : IGeneralInterfaces
    {
        #region General Db Primary Dependencies

        private readonly WebContext _context = context;

        #endregion

        #region Image Methods

        public Task<bool> DeleteImageFileAsync(string folderPath, string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return Task.FromResult(false);

            string deletePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                folderPath,
                fileName);

            if (File.Exists(deletePath))
            {
                File.Delete(deletePath);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public async Task<string> UploadImageFileAsync(Stream fileStream, string originalFileName, string folderPath)
        {
            if ((fileStream == null) || (fileStream.Length == 0))
            {
                Console.WriteLine("UploadImageFileAsync called with empty stream");
                return null;
            }

            Console.WriteLine($"Uploading file: {originalFileName} to {folderPath}");

            //if ((fileStream == null) || (fileStream.Length == 0))
            //    return null;

            // Generate unique file name
            string uniqueFileName = NameGenerator.GenerateUniqName() +
                                    Path.GetExtension(originalFileName);

            // Build save path
            string savePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                folderPath,
                uniqueFileName);

            // Ensure directory exists
            string directoryPath = Path.GetDirectoryName(savePath);

            #region Other Check Directory Methods

            //if (string.IsNullOrWhiteSpace(directoryPath))
            //    throw new ArgumentNullException(nameof(directoryPath), "مسیر ذخیره فایل نمی‌تواند خالی باشد.");

            //Directory.CreateDirectory(directoryPath);

            //if (!Directory.Exists(directoryPath) && directoryPath != null)
            //    Directory.CreateDirectory(directoryPath);

            #endregion

            if (!string.IsNullOrWhiteSpace(directoryPath))
                Directory.CreateDirectory(directoryPath);

            // Save file

            #region Save Method 1 and Multiple File

            /*
             * This style has been around since C# 1.
             * Automatically calls Dispose at the end of the scope.
             * It still works in async code, but since Dispose is itself sync, it may cause blocking in some cases (not important except in high concurrency IO).
             */
            //using (var stream = new FileStream(savePath, FileMode.Create))
            //{
            //    await fileStream.CopyToAsync(stream);
            //}

            #endregion

            #region Save Method 2 On C# 8

            /*
             * Introduced in C# 8.
             * Calls DisposeAsync if the class is IAsyncDisposable.
             * For classes like FileStream (which supports IAsyncDisposable in .NET Core 3 and later), it is the most efficient and cleanest way.
             */
            //await using var stream = new FileStream(savePath, FileMode.Create);
            //await fileStream.CopyToAsync(stream);

            #endregion

            #region Save Method 3 Look Like The Same Verbose await using But If Not Supported On Older Methods on Older C# Versions

            /*
             * This method is exactly equivalent to await using but is more verbose.
             * This is a good fallback when you don't want to use await using or are in an environment where C# 8 is not enabled.
             * It is also recommended in Resharper as an equivalent method to await using.
             */

            //var stream = new FileStream(savePath, FileMode.Create);
            //try
            //{
            //    await fileStream.CopyToAsync(stream);
            //}
            //finally
            //{
            //    if (true) await stream.DisposeAsync();
            //}

            #endregion

            await using var stream = new FileStream(savePath, FileMode.Create);
            await fileStream.CopyToAsync(stream);

            return uniqueFileName;
        }

        #endregion

        #region Category Methods

        public async Task<Categories> FindCategoryByCategoryIdAsync(ulong categoryId) =>
            await _context.Categories.FindAsync(categoryId);

        public async Task<List<CategoryViewModel>> GetAllCategoriesToListAsync()
        {
            try
            {
                return await _context.Categories
                    .Select(c => new CategoryViewModel()
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName
                    }).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<bool> CreateCategoryFromAdminPanelAsync(CategoryViewModel model)
        {
            if (model == null) return false;

            var category = new Categories()
            {
                CategoryName = model.CategoryName,
                CreateDate = DateTime.UtcNow
            };

            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryViewModel> GetAllDataCategoriesBeforeEditAsync(ulong categoryId)
        {
            try
            {
                var category = await FindCategoryByCategoryIdAsync(categoryId);

                if (category != null)
                {
                    return new CategoryViewModel()
                    {
                        CategoryId = categoryId,
                        CategoryName = category.CategoryName
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return null;
        }

        public async Task<bool> UpdateCategoryFromAdminPanelAsync(CategoryViewModel model, ulong categoryId)
        {
            var categoryData = await FindCategoryByCategoryIdAsync(categoryId);
            if (categoryData != null)
            {
                categoryData.CategoryName = model.CategoryName;
                categoryData.UpdateDate = DateTime.Now;

                _context.Update(categoryData);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteCategoryFromAdminPanelAsync(ulong categoryId)
        {
            var categoryData = await FindCategoryByCategoryIdAsync(categoryId);
            if (categoryData != null)
            {
                _context.Remove(categoryData);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        #endregion

        #region Category Selection Methods

        public async Task<SelectList> GetCategorySelectListAsync()
        {
            var categoryData = await _context.Categories
                .Select(c => new CategoryDataCombineViewModel()
                {
                    CategoryName = c.CategoryName,
                    CategoryId = c.CategoryId
                }).Distinct().ToListAsync();

            if (categoryData == null)
                throw new Exception("Role Data Was Not Found");

            return new SelectList(categoryData, nameof(CategoryDataCombineViewModel.CategoryId), nameof(CategoryDataCombineViewModel.CategoryName));
        }

        public async Task<SelectList> GetCategorySelectListByCategoryIdAsync(ulong categoryId)
        {
            var categoryData = await _context.Categories
                .Select(c => new CategoryDataCombineViewModel()
                {
                    CategoryName = c.CategoryName,
                    CategoryId = c.CategoryId
                }).Distinct().ToListAsync();

            if (categoryData == null)
                throw new Exception("Role Data Was Not Found");

            return new SelectList(categoryData, nameof(CategoryDataCombineViewModel.CategoryId), nameof(CategoryDataCombineViewModel.CategoryName),
                categoryId);
        }

        #endregion
    }
}
