using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PassPortable
{
    /// <summary>
    /// Interaktionslogik für AddSiteWindow.xaml
    /// </summary>
    public partial class AddSiteWindow : Window
    {
        public AddSiteWindow()
        {
            InitializeComponent();

            DataContext = new AddSiteView(this);
        }
    }
}
