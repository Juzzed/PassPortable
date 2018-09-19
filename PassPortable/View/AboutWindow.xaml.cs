using System.Windows.Controls;

namespace PassPortable
{ 
    /// <summary>
    /// Interaktionslogik für OptionWindow.xaml
    /// </summary>
    public partial class AboutWindow : UserControl
    {
        public AboutWindow()
        {
            InitializeComponent();

            DataContext = new AboutView();
        }
    }
}
