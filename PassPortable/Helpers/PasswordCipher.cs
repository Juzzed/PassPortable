using PassPortable.Helpers;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace PassPortable
{
    public class PasswordCipher
    {
        public static string Encrypt(string plainPassword)
        {
            return Encoding.Unicode.GetString(AES.AES_Encrypt(Encoding.Unicode.GetBytes(plainPassword), Encoding.Unicode.GetBytes(LogInView.CurrentUser.Password)));
        }

        public static string Decrypt(string encryptedPassword)
        {
            return Encoding.Unicode.GetString(AES.AES_Decrypt(Encoding.Unicode.GetBytes(encryptedPassword), Encoding.Unicode.GetBytes(LogInView.CurrentUser.Password)));
        }

        public static string ConvertSecureStringToHash(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                return HashHandler.Hash(Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(securePassword)));
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
