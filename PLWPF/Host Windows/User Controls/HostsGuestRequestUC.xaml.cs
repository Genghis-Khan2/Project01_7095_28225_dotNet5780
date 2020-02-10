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

namespace PLWPF.Host_Windows.User_Controls
{
    /// <summary>
    /// Interaction logic for HostsGuestRequestUC.xaml
    /// </summary>
    public partial class HostsGuestRequestUC : UserControl
    {
        private readonly HostMenu caller;
        private readonly BE.GuestRequest gr;
        private readonly BE.Host host;
        public HostsGuestRequestUC(HostMenu caller, BE.GuestRequest guestRequest, BE.Host host)
        {
            InitializeComponent();
            GRName.Content = guestRequest.Requester.PrivateName + " " + guestRequest.Requester.FamilyName;
            Duration.Content = string.Format("{0}.{2} - {1}.{3}", guestRequest.EntryDate.Day, guestRequest.ReleaseDate.Day,
                guestRequest.EntryDate.Month, guestRequest.ReleaseDate.Month);
            MailAddress.Content = guestRequest.Requester.MailAddress;
            gr = guestRequest;
            this.host = host;
            this.caller = caller;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new GuestRequestInfo(gr, host);
            window.ShowDialog();
            BE.HostingUnit hu = null;
            window.Closing += (s, args) =>
            {
                hu = window.GetHostingUnit();
                if (hu != null)
                {
                    GlobalVars.myBL.UpdateHostingUnit(hu, hu.HostingUnitKey);
                    caller.Complete_Refresh();
                }
            };
        }
    }
}
