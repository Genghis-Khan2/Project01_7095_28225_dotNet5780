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
using System.Windows.Shapes;

namespace PLWPF.Admin_Windows.Info_Windows
{
    /// <summary>
    /// Interaction logic for AdminHostInfoWindow.xaml
    /// </summary>
    public partial class AdminHostInfoWindow : Window
    {
        public AdminHostInfoWindow(BE.Host host)
        {
            InitializeComponent();
            LoadData(host);
        }

        private void LoadData(BE.Host host)
        {
            Key.Content = host.HostKey;
            Name.Content = host.PrivateName + " " + host.FamilyName;
            PhoneNum.Content = host.PhoneNumber;
            MailAddress.Content = host.MailAddress;
            BankAccount.Content = host.BankAccountNumber;
            HiddenLab.Content = host.CollectionClearance;
        }
    }
}
