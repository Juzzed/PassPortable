using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PassPortable
{
    class AboutView : ViewModelBase
    {

        public RelayCommand GithubCommand { get; set; }
        public RelayCommand ContactCommand { get; set; }
        public string VersionNumber { get; set; }
        public string Disclamer { get; set; }

        public AboutView()
        {
            VersionNumber = "Version 0.2.4.21 (Alpha)\n";
            Disclamer = "This Software is not in a proper working state so use it at your own risk!\n";
            GithubCommand = new RelayCommand(Github);
            ContactCommand = new RelayCommand(Contact);
        }

        public void Github()
        {
            System.Diagnostics.Process.Start("https://github.com/Juzzed/PassPortable");
        }

        public void Contact()
        {
            MessageBox.Show("https://github.com/Juzzed/");
        }

    }
}
