using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using GalaSoft.MvvmLight;
using PassPortable.Properties;

namespace PassPortable
{
    [Serializable]
    public class Site : ObservableObject
    {
        public enum Tag
        {
            [Description("Web")]
            Web,
            [Description("TV")]
            TV,
            [Description("Google")]
            Google,
            [Description("Not Specified")]
            NotSpecified
        }

        public static ObservableCollection<Site> Sites { get; set; }

        public Site(String url, String username, String password, Tag tag, String salt)
        {
            
            Url = url;
            Username = username;
            Password = password;
            Salt = salt;
            TagId = tag;
        }


        private string _salt;
        public string Salt
        {
            get
            {
                return _salt;
            }
            private set
            {
                if(_salt != value)
                {
                    _salt = value;
                }
            }
        }

        public string Url
        {
            get
            {
                return _Url;
            }
            set
            {
                if (_Url != value)
                {
                    _Url = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _Url;

        public string Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (_Username != value)
                {
                    _Username = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _Username;

        public string Password
        {
            get
            {
                IntPtr valuePtr = IntPtr.Zero;
                try
                {
                    valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_Password);
                    return Marshal.PtrToStringUni(valuePtr);
                }
                finally
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
                }
            }
            set
            {
                var secure = new SecureString();
                foreach (char c in value)
                {
                    secure.AppendChar(c);
                }

                if (_Password != secure)
                {
                    _Password = secure;
                    RaisePropertyChanged();
                }
            }
        }
        private SecureString _Password;

        public Tag TagId { get; set; }
    }
}
