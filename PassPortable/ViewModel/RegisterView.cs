using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Controls;

namespace PassPortable
{
    class RegisterView : ViewModelBase
    {
        private Window _window;

        public string Username { get; set; } = "";
        public string Password { get; set; } = "";


        public RelayCommand<PasswordBox> RegisterCommand { get; set; }

        public RegisterView(Window window)
        {
            this._window = window;
            RegisterCommand = new RelayCommand<PasswordBox>(Register);
        }

        public void Register(PasswordBox parameter)
        {
            if(Username == "")
            {
                MessageBox.Show("cant be empty");
                return;
            }

            User user = new User();

            if (parameter.SecurePassword != null)
            {
                user.Password = PasswordCipher.ConvertSecureStringToHash(parameter.SecurePassword);
            }

            user.Username = HashHandler.Hash(Username);

            string keyCombination = HashHandler.Hash(user.Username + user.Password + "YOLOSWAG1337");

            MessageBox.Show(UserHandler.RegisterUser(user));

            _window.Close();
        }




    }
}
