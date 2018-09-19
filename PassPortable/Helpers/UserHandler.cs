using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassPortable
{
    class UserHandler
    {
        public static bool Authenticate(User user)
        {

            //JObject jObject = JObject.Parse("");
            //JToken[] data = jObject["User"].Children().ToArray();

            //List<User> users = JsonHandler.GetUsers()[0].Password;

            if (JsonHandler.ValidateUser(user) != null)
            {
                LogInView.CurrentUser = JsonHandler.ValidateUser(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string RegisterUser(User user)
        {
            JsonHandler.AddUser(user);
            return "Created";
        }

        public static void SaveSiteToJson()
        {
            JsonHandler.AddUser(LogInView.CurrentUser);
        }

        public static string EncryptPassword(string password) 
        {
            return PasswordCipher.Encrypt(password);
        }

        public static string DecryptPassword(string password)
        {
            return PasswordCipher.Decrypt(password);
        }

    }
}
