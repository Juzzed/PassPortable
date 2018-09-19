using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Security;

namespace PassPortable
{
    [Serializable]
    public class User : ObservableObject
    {
        public string Username { get; set; }

        public ObservableCollection<Site> Sites{ get; set; }

        private SecureString _password;
        public string Password
        {
            get
            {
                IntPtr valuePtr = IntPtr.Zero;
                try
                {
                    valuePtr = Marshal.SecureStringToGlobalAllocUnicode(_password);
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

                if (_password != secure)
                {
                    _password = secure;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
