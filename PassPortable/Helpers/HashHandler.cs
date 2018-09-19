using System.Security.Cryptography;
using System.Text;

namespace PassPortable
{
    class HashHandler
    {
        public static string Hash(string inputString)
        {
            using (var deriveBytes = new Rfc2898DeriveBytes(inputString, 20))
            {
                using (SHA256 sha256 = SHA256Managed.Create())
                {
                    return GetStringFromHash(sha256.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
                }
            }
        }

        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}