using GalaSoft.MvvmLight.Command;
using PassPortable.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PassPortable
{
    public class LogInView : ViewBase
    {
        #region private Variables
        private Window _window;

        //Margin für window schatten
        private int _outerMarginSize = 10;
        //Radius von window ecken
        private int _windowRadius = 10;

        #endregion

        #region Properties

        public static User CurrentUser { get; set; }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public double WindowMinumumWidth { get; set; } = 500;

        public double WindowMinumumHeight { get; set; } = 400;

        public bool BorderLess { get { return (_window.WindowState == WindowState.Maximized); } }

        public int ResizeBorder { get { return BorderLess ? 0 : 6; } }

        public int OuterMarginSize
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
            }
            set
            {
                _outerMarginSize = value;
            }
        }

        public Thickness OuterMarginSizeThickness { get { return new Thickness(OuterMarginSize); } }

        public int WindowRadius
        {
            get
            {
                return _window.WindowState == WindowState.Maximized ? 0 : _windowRadius;
            }
            set
            {
                _windowRadius = value;
            }
        }

        public CornerRadius WindowCornerRadius { get { return new CornerRadius(WindowRadius); } }

        public int TitleHeight { get; set; } = 30;

        public Visibility MainImageVisibility { get; set; } = Visibility.Visible;

        public GridLength TitleHeightGridLenght { get { return new GridLength(TitleHeight + ResizeBorder); } }

        public PasswordBox box { get; set; }

        #endregion

        #region Comamnds

        public ICommand CloseCommand { get; set; }

        public RelayCommand<PasswordBox> LogInCommand { get; set; }

        public RelayCommand RegisterNewLocalUserCommand { get; set; }

        public RelayCommand ToBeDoneCommand { get; set; }

        public RelayCommand NewSiteCommand { get; set; }

        public string Username { get; set; }

        public bool OpenRegister { get; set; }
        #endregion

        public LogInView(Window window)
        {
            this._window = window;

            _window.StateChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(OuterMarginSize).ToString());
                RaisePropertyChanged(nameof(OuterMarginSizeThickness));
                RaisePropertyChanged(nameof(WindowRadius));
                RaisePropertyChanged(nameof(WindowCornerRadius));
            };

            CloseCommand = new RelayCommand(Close);
            LogInCommand = new RelayCommand<PasswordBox>(LogIn);
            ToBeDoneCommand = new RelayCommand(ToBeDone);
            RegisterNewLocalUserCommand = new RelayCommand(RegisterNewLocalUser);



            //Fix Rezise issues
            var resizer = new WindowResizer(_window);

            JsonHandler.CreateConfigFile();
            //XmlHandler.CreateXml();
        }

        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;


        private void Close()
        {
            this._window.Close();
        }

        private void LogIn(PasswordBox parameter)
        {
            User user = new User();
            if (parameter.SecurePassword != null)
            {
                user.Password = PasswordCipher.ConvertSecureStringToHash(parameter.SecurePassword);
            }
            
            user.Username = HashHandler.Hash(Username);
           

            //Check if user is valid
            if (UserHandler.Authenticate(user) == true)
            {
                MainWindow main = new MainWindow();
                main.Show();
                _window.Close();
            }
            else
            {
                MessageBox.Show("Wrong Login");
            }
        }


        private void RegisterNewLocalUser()
        {
            Window register = new RegisterWindow();
            register.ShowDialog();
        }

        private void ToBeDone()
        {
            MessageBox.Show("Not implemented in this version. Stay tuned");
        }
    }
}
