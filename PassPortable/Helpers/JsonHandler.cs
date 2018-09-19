using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.AccessControl;
using System.Collections.ObjectModel;
using PassPortable.Helpers;
using System.Text;
using System.Security.Cryptography;

namespace PassPortable
{
    public static class JsonHandler
    {
        private static string path = System.Reflection.Assembly.GetEntryAssembly().Location.Replace("PassPortable.exe", "");

        public static string dataPath = path + "ProgramData\\Config.ztf";

        public static void CreateConfigFile(string username = "test", string password = "test")
        {
            Directory.CreateDirectory(path + "ProgramData\\");
            if (!File.Exists(dataPath))
            {
                using (StreamWriter writer = new StreamWriter(dataPath, true))
                {
                    writer.WriteLine("NO DATA. THIS IS A BLANK CONFIG FILE AND NOT ENCRYPTED. CREATE A USER TO FILL THIS FILE WITH REAL DATA ;)");
                }
                File.ReadAllText(dataPath);
            }


        }

        //CURRENTLY OVERWRITES FILE
        //LATER ON IT WILL VALIDATE ALL THE USERS
        public static void AddUser(User user)
        {

            UserData temp = new UserData();
            temp.Userdata = new ObservableCollection<User>();
            temp.Userdata.Add(user);

            //Encrypt file
            byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(temp));

            byte[] keyCombinationBytes = Encoding.UTF8.GetBytes(user.Username + "$!sdakw" + user.Password + "randomData");

            byte[] bytesEncrypted = AES.AES_Encrypt(bytesToBeEncrypted, SHA512.Create().ComputeHash(keyCombinationBytes));

            keyCombinationBytes = new byte[] { 1 };

            File.WriteAllBytes(dataPath, bytesEncrypted);

            //USE THIS FOR DEBBUGING THE FILE
            //File.WriteAllText(dataPath, JsonConvert.SerializeObject(temp));


        }

        public static User ValidateUser(User user)
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            byte[] bytesToBeDecrypted = File.ReadAllBytes(dataPath);

            byte[] keyCombinationBytes = Encoding.UTF8.GetBytes(user.Username + "$!sdakw" + user.Password + "randomData");

            byte[] bytesDecrypted = AES.AES_Decrypt(bytesToBeDecrypted, SHA512.Create().ComputeHash(keyCombinationBytes));

            keyCombinationBytes = new byte[] { 1 };

            //USE THIS FOR THE FILE DEBBUGING
            //string json = File.ReadAllText(dataPath);

            string json = Encoding.UTF8.GetString(bytesDecrypted);

            if (json == "" || json == null)
            {
                return null;
            }

            JToken[] userss;
            try
            {
                JObject jObj = JObject.Parse(json.ToString());
                userss = jObj["Userdata"].Children().ToArray();
            }
            catch
            {
                return null;
            }

            foreach (var i in userss)
            {
                User tmp = JsonConvert.DeserializeObject<User>(i.ToString());

                if ((tmp.Password == user.Password) && (tmp.Username == tmp.Username))
                {
                    json = null;
                    bytesDecrypted = null;
                    return tmp;
                }
            }

            json = null;
            bytesDecrypted = null;

            return null;
        }
    }
}

