using System.Windows;

namespace PassPortable
{
    /// <summary>
    /// Interaktionslogik für MainWindowTemplate.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new WindowView(this);
        }
    }
}
