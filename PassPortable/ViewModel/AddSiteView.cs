using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.Generic;
using System.Windows;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PassPortable
{
    class AddSiteView : ViewModelBase
    {
        private Window _window { get; set; }

        public IList<Site.Tag> TagIds
        {
            get
            {
                return Enum.GetValues(typeof(Site.Tag)).Cast<Site.Tag>().ToList<Site.Tag>();
            }
        }
        public Site.Tag TagId { get; set; }

        public string Username { get; set; } = "";

        public string Password { get; set; } = "";

        public string Url { get; set; } = "";


        public RelayCommand AddSiteCommand { get; set; }

        public RelayCommand ClosePopUpCommand { get; set; }

        public AddSiteView(Window window)
        {
            _window = window;
            AddSiteCommand = new RelayCommand(AddSite);
            ClosePopUpCommand = new RelayCommand(ClosePupUp);
        }

        public void AddSite()
        {
            if (Username == "" || Password == "" || Url == "")
            {
                MessageBox.Show("One of the boxes is empty. Please fill");
                return;
            }


            byte[] salt = new byte[128];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            Site site = new Site(Url, Username, PasswordCipher.Encrypt(Password + Convert.ToString(salt)), TagId, Convert.ToString(salt));

            if (LogInView.CurrentUser.Sites == null)
            {
                LogInView.CurrentUser.Sites = new ObservableCollection<Site> { site };
            }
            else
            {
                LogInView.CurrentUser.Sites.Add(site);
            }

            _window.Close();
        }

        public void ClosePupUp()
        {
            _window.Close();
        }

    }
}