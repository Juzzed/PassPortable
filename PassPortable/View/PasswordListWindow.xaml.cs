using System.Windows.Controls;

namespace PassPortable
{
    /// <summary>
    /// Interaktionslogik für LoginPageWindow.xaml
    /// </summary>
    public partial class PasswordListWindow
    {
        public PasswordListWindow()
        {
            InitializeComponent();

            DataContext = new PasswordListView();
        } 
    }
}
