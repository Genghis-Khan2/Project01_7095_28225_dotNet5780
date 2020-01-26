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

namespace PLWPF.Admin_Windows
{
    /// <summary>
    /// Interaction logic for AdminGuestRequestUC.xaml
    /// </summary>
    public partial class AdminGuestRequestUC : UserControl
    {
        private readonly BE.GuestRequest gr;
        public AdminGuestRequestUC(BE.GuestRequest guestRequest)
        {
            InitializeComponent();
            GRName.Content = guestRequest.Requester.PrivateName + " " + guestRequest.Requester.FamilyName;
            Duration.Content = string.Format("{0}.{2} - {1}.{3}", guestRequest.EntryDate.Day, guestRequest.ReleaseDate.Day,
                guestRequest.EntryDate.Month, guestRequest.ReleaseDate.Month);
            MailAddress.Content = guestRequest.Requester.MailAddress;
            gr = guestRequest;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new AdminGRInfoWindow(gr);
            window.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.RemoveGuestRequest(gr.GuestRequestKey);
        }
    }
}
