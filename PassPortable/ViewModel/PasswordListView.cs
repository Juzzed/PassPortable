using System.Collections.ObjectModel;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PassPortable
{
    public class PasswordListView : ViewModelBase
    {
        public RelayCommand DeleteSiteCommand { get; set; }
        public RelayCommand CopyPasswordCommand { get; set; }
        public RelayCommand NewSiteCommand { get; set; }

        public bool OpenPopUp { get; set; }

        public string _countSites;
        public string CountSites
        {
            get
            {
                return _countSites;
            }
            set
            {
                if(_countSites != value)
                {
                    _countSites = value;
                    RaisePropertyChanged();
                }
            }
        }

        public PasswordListView()
        {
            
            SitesBind = new ObservableCollection<Site>();
            RefreshSites();

            NewSiteCommand = new RelayCommand(NewSite);
            DeleteSiteCommand = new RelayCommand(DeleteSite);
            CopyPasswordCommand = new RelayCommand(CopyPassword);
        }

        private bool _deleteEnabled = false;
        public bool DeleteEnabled
        {
            get
            {
                return _deleteEnabled;
            }
            set
            {
                if(_deleteEnabled != value)
                {
                    _deleteEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        private bool _copyEnabled = false;
        public bool CopyEnabled
        {
            get
            {
                return _copyEnabled;
            }
            set
            {
                if (_copyEnabled != value)
                {
                    _copyEnabled = value;
                    RaisePropertyChanged();
                }
            }
        }

        ObservableCollection<Site> _sitesBind;
        public ObservableCollection<Site> SitesBind
        {
            get
            {
                return _sitesBind;
            }
            set
            { 
                _sitesBind = new ObservableCollection<Site>();
                _sitesBind = value;
                RaisePropertyChanged();
            }
        }

        private void DeleteSite()
        {
            if (SelectedSite != null)
            {
                LogInView.CurrentUser.Sites.Remove(_selectedSite);
                RefreshSites();
                Password = "";
            }
        }


        private void NewSite()
        {
            AddSiteWindow addSite = new AddSiteWindow();
            System.Drawing.Rectangle resolution = Screen.PrimaryScreen.Bounds;
            addSite.Top = (resolution.Bottom / 2) - (325 / 2);
            addSite.Left = (resolution.Right / 2) - (251 / 2);
            addSite.ShowDialog();
            RefreshSites();
        }

        private void CopyPassword()
        {
            if (SelectedSite != null)
            {
                System.Windows.Clipboard.SetText(PasswordCipher.Decrypt(SelectedSite.Password).Replace(SelectedSite.Salt, ""));
            }
        }

        private void RefreshSites()
        {
            var sitesList = LogInView.CurrentUser.Sites;
            try
            {
                SitesBind = new ObservableCollection<Site>();
                foreach (var site in sitesList)
                {
                    SitesBind.Add(site);
                }
                UserHandler.SaveSiteToJson();
                CountSites = "Sites : " + SitesBind.Count.ToString();
            }
            catch
            {
                return;
            }
        }

        //Binded Variables
        #region ShowPasswordChecked
        bool _showPasswordChecked;
        public bool ShowPasswordChecked
        {
            get
            {
                return _showPasswordChecked;
            }
            set
            {
                if (_showPasswordChecked != value)
                {
                    if (value == true && SelectedSite != null)
                    {
                        DialogResult dialogResult = MessageBox.Show("Showing password in plain text isn't secure. Are you sure?", "Warning!", MessageBoxButtons.OKCancel);

                        if (dialogResult == DialogResult.OK)
                        {
                            Password = PasswordCipher.Decrypt(SelectedSite.Password).Replace(SelectedSite.Salt, "");
                            _showPasswordChecked = value;
                            RaisePropertyChanged("ShowPasswordChecked");
                        }
                        else if (dialogResult == DialogResult.Cancel)
                        {
                            Password = "**************";
                            _showPasswordChecked = false;
                            RaisePropertyChanged("ShowPasswordChecked");
                        }
                    }
                    else if (value == false)
                    {
                        Password = "**************";
                        _showPasswordChecked = value;
                        RaisePropertyChanged("ShowPasswordChecked");
                    }

                }
            }
        }


        #endregion
        #region SelectedSite
        Site _selectedSite;
        public Site SelectedSite
        {
            get
            {
                return _selectedSite;
            }
            set
            {
                if (_selectedSite != value)
                {
                    _selectedSite = value;
                    Password = "**************";
                    RaisePropertyChanged();
                    ShowPasswordChecked = false;
                }

                if(_selectedSite == null)
                {
                    CopyEnabled = false;
                    DeleteEnabled = false;
                }
                else
                {
                    CopyEnabled = true;
                    DeleteEnabled = true;
                }
            }
        }
        #endregion
        #region Password
        string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }
        #endregion

    }
}
