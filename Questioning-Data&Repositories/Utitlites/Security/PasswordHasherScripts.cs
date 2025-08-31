using System.Security.Cryptography;
using System.Text;

namespace Questioning_Data_Repositories.Utitlites.Security
{
    public static class PasswordHasherScripts
    {
        private static readonly string Salt = "some_random_salt"; // باید مقدار Salt را تصادفی ایجاد کنید

        public static async Task<string> EncodePasswordSha256Async(string pass)
        {
            // استفاده از Salt برای امنیت بیشتر
            string saltedPassword = pass + Salt;

            using SHA256 sha256 = SHA256.Create();
            byte[] originalBytes = Encoding.UTF8.GetBytes(saltedPassword);

            byte[] hashedBytes = await Task.Run(() => sha256.ComputeHash(originalBytes));

            StringBuilder sb = new StringBuilder();
            int counter = 0;

            foreach (byte b in hashedBytes)
            {
                sb.Append(b.ToString("x2"));
                counter++;

                // بعد از هر چهار کاراکتر یک خط تیره اضافه می‌کنیم
                if (counter % 4 == 0 && counter != hashedBytes.Length)
                {
                    sb.Append('-');
                }
            }

            return sb.ToString();
        }

        public static async Task<bool> VerifyPasswordAsync(this string hashedPassword, string providedPassword)
        {
            string hashedProvidedPassword = await EncodePasswordSha256Async(providedPassword);
            return hashedPassword == hashedProvidedPassword;
        }

    }
}