using PLWPF.Admin_Windows.Info_Windows;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly AdminWindow caller;
        public AdminHostUC(AdminWindow caller, BE.Host host)
        {
            InitializeComponent();
            Name.Content = host.PrivateName + " " + host.FamilyName;
            MailAddress.Content = host.MailAddress;
            PhoneNumber.Content = host.PhoneNumber;
            this.host = host;
            this.caller = caller;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new AdminHostInfoWindow(host);
            window.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var HUs = from i in GlobalVars.myBL.GetAllHostingUnits()
                          select i;

                foreach (var i in HUs)
                {
                    if (i.Owner.HostKey == host.HostKey)
                    {
                        GlobalVars.myBL.RemoveHostingUnit(i.HostingUnitKey);
                    }
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }

            FR.FR_Imp.GetFR().RemoveHostFromFile(FR.FR_Imp.GetFR().GetHostUserNameFromKey(host.HostKey));

            caller.Complete_Refresh();
        }
    }
}
