using System.Windows;

namespace PassPortable
{
    /// <summary>
    /// Interaktionslogik für RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();

            DataContext = new RegisterView(this);
        }
    }
}
