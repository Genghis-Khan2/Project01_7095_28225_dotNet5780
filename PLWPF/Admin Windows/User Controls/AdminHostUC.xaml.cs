using PLWPF.Admin_Windows.Info_Windows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF.Admin_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for AdminHostUC.xaml
    /// </summary>
    public partial class AdminHostUC : UserControl
    {
        private BE.Host host;
        public AdminHostUC(BE.Host host)
        {
            InitializeComponent();
            Name.Content = host.PrivateName + " " + host.FamilyName;
            MailAddress.Content = host.MailAddress;
            PhoneNumber.Content = host.PhoneNumber;
            this.host = host;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new AdminHostInfoWindow(host);
            window.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FR.FR_Imp.GetFR().RemoveHostFromFile(host.HostKey);
        }
    }
}
