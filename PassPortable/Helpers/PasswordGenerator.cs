using System;
using System.Security.Cryptography;
using System.Text;

namespace PassPortable
{
    class PasswordGenerator
    {

        public static string GeneratePassword(int passwordLenght, bool upperLowerCase, bool useSybols)
        {

            string charset = "abcdefghijklmnopqrstuvwxyzYZ1234567890";

            if (upperLowerCase == true)
            {
                charset = charset + "ABCDEFGHIJKLMNOPQRSTUVWX";
            }

            if (useSybols == true)
            {
                charset = charset + "!§$%/()=?*+#_-{[]}~.:,;";
            }

            StringBuilder res = new StringBuilder();

            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();


            byte[] uintBuffer = new byte[sizeof(uint)];

            while (passwordLenght-- > 0)
            {
                random.GetBytes(uintBuffer);
                uint num = BitConverter.ToUInt32(uintBuffer, 0);
                res.Append(charset[(int)(num % (uint)charset.Length)]);
            }

            return res.ToString();
        }
    }
}
