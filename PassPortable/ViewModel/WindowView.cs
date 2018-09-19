using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace PassPortable
{
    public class WindowView : ViewModelBase
    {
        #region private Variables
        private Window _window;

        //Margin für window schatten
        private int _outerMarginSize = 10;
        //Radius von window ecken
        private int _windowRadius = 10;

        #endregion

        #region Properties

        public double Top { get; set; }
        public double Left { get; set; }

        public Point point { get; set; }

        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        public double WindowMinumumWidth { get; set; } = 500;

        public double WindowMinumumHeight { get; set; } = 400;

        public bool BorderLess { get { return (_window.WindowState == WindowState.Maximized); } }

        public int ResizeBorder { get { return BorderLess ? 0 : 6; } }

        public Thickness ResizeBorderThickness { get { return new Thickness(ResizeBorder + OuterMarginSize); } }

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

        ApplicationPage _currentPage;
        public ApplicationPage CurrentPage {
            get
            {
                return _currentPage;
            }
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    RaisePropertyChanged(nameof(_currentPage));
                }
            }
        }

        Visibility _loginPageVisibility;
        public Visibility LoginPageVisibility
        {
            get
            {
                return _loginPageVisibility;
            }
            set
            {
                if (_loginPageVisibility != value)
                {
                    _loginPageVisibility = value;
                    RaisePropertyChanged("LoginPageVisibility");
                }
            }
        }

        Visibility _mainFrameVisibility;
        public Visibility MainFrameVisibility
        {
            get
            {
                return _mainFrameVisibility;
            }
            set
            {
                if (_mainFrameVisibility != value)
                {
                    _mainFrameVisibility = value;
                    RaisePropertyChanged("MainFrameVisibility");
                }
            }
        }

        #endregion

        #region Comamnds

        public ICommand MinimizeCommand { get; set; }

        public ICommand MaximizeCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        public ICommand MenuCommand { get; set; }

        public RelayCommand ChangePageCommand { get; set; }

        #endregion


        public WindowView(Window window)
        {
            CurrentPage = ApplicationPage.MainMenuWindow;
            this._window = window;

            _window.StateChanged += (sender, e) =>
            {
                RaisePropertyChanged(nameof(ResizeBorderThickness));
                RaisePropertyChanged(nameof(OuterMarginSize));
                RaisePropertyChanged(nameof(OuterMarginSizeThickness));
                RaisePropertyChanged(nameof(WindowRadius));
                RaisePropertyChanged(nameof(WindowCornerRadius));
            };

            MinimizeCommand = new RelayCommand(Minimize);
            MaximizeCommand = new RelayCommand(Maximize);
            CloseCommand = new RelayCommand(Close);
            MenuCommand = new RelayCommand(Menu);

            point = new Point(this.Left, this.Top);

            //Fix Rezise issues
            var resizer = new WindowResizer(_window);
        }

        private void Minimize()
        {
            _window.WindowState = WindowState.Minimized;
        }

        private void Maximize()
        {
            _window.WindowState ^= WindowState.Maximized;
        }

        private void Close()
        {
            this._window.Close();
        }

        private void Menu()
        {
            SystemCommands.ShowSystemMenu(_window, _window.PointToScreen(Mouse.GetPosition(_window)));
        }
    }
}
