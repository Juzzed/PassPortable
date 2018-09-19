using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PassPortable
{
    class GeneratePasswordView : ViewModelBase
    {
        public GeneratePasswordView()
        {
            PasswordLenght = 16;
            GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        }

        public RelayCommand GeneratePasswordCommand { get; set; }

        private void GeneratePassword()
        {
            GeneratedPassword = PasswordGenerator.GeneratePassword(PasswordLenght, LowerUpperChecked, SymbolsChecked);
        }

        //Binded Variables
        #region GeneratedPassword
        string _generatedPassword;
        public string GeneratedPassword
        {
            get { return _generatedPassword; }
            set
            {
                _generatedPassword = value;
                RaisePropertyChanged("GeneratedPassword");
            }
        }
        #endregion
        #region PasswordLenght
        int _passwordLenght;
        public int PasswordLenght
        {
            get { return _passwordLenght; }
            set
            {
                _passwordLenght = value;
                RaisePropertyChanged("PasswordLenght");
            }
        }
        #endregion
        #region LowerUpperChecked
        bool _lowerUpperChecked;
        public bool LowerUpperChecked
        {
            get { return _lowerUpperChecked; }
            set
            {
                _lowerUpperChecked = value;
                RaisePropertyChanged("LowerUpperChecked");
            }
        }
        #endregion
        #region SymbolsChecked
        bool _symbolsChecked;
        public bool SymbolsChecked
        {
            get { return _symbolsChecked; }
            set
            {
                _symbolsChecked = value;
                RaisePropertyChanged("SymbolsChecked");
            }
        }
        #endregion
    }
}
