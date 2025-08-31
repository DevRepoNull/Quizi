using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;

namespace Questioning_Data_Repositories.Utitlites.Security
{
    public static class ImageValidator
    {
        private static readonly string[] SupportedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

        public static async Task<bool> IsImageAsync(this IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension) || !IsSupportedImageExtension(extension.ToLower()))
                return false;

            try
            {
                await using var stream = file.OpenReadStream();
                using var image = await Image.LoadAsync(stream);
                return image != null;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsSupportedImageExtension(string extension)
        {
            return Array.Exists(SupportedExtensions, ext => ext == extension);
        }
    }
}