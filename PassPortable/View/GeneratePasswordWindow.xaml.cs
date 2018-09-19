using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;


namespace PassPortable
{
    /// <summary>
    /// Interaktionslogik für GeneratePasswordView.xaml
    /// </summary>
    public partial class GeneratePasswordWindow : UserControl
    {
        public GeneratePasswordWindow()
        {
            InitializeComponent();
            DataContext = new GeneratePasswordView();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
