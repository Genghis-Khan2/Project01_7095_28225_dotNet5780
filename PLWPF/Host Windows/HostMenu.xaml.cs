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
using System.Linq;
using FR;
using PLWPF.Host_Windows;
using PLWPF.Host_Windows.User_Controls;
using System.Threading;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostMenu.xaml
    /// </summary>
    public partial class HostMenu : Window
    {
        readonly BE.Host host;
        public HostMenu(BE.Host host)
        {
            InitializeComponent();
            this.host = host;
            Complete_Refresh(); // TODO: Check if this function works by adding hardcoded guest request into list
        }

        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            var win = new CreateHostingUnit(host);
            win.Show();
            win.Closing += (s, args) =>
            {
                Complete_Refresh();
            };
        }

        private void LoadGuestRequests()
        {
            GuestRequestStack.Children.Clear();
            GuestRequestStack.Children.Add(Resources["GuestTitlesBar"] as Grid);

            List<BE.GuestRequest> list = new List<BE.GuestRequest>();

            try
            {
                var li = from i in CreateAccount.myBL.getHostingUnitByHost()
                         where i.Key.HostKey == FR_Imp.GetFR().GetHostKey(LoginPage.UserName)
                         select new { Hostingunits = i };
                foreach (var i in li)
                {
                    foreach (var stuff in i.Hostingunits)
                    {
                        list.AddRange(CreateAccount.myBL.GetMatchingGuestRequests(stuff));

                    }
                }

                var toRemDup = from j in list
                               select j;
                list = toRemDup.Distinct(new BE.GuestRequestComparer()).ToList();

                foreach (var i in list)
                {
                    GuestRequestStack.Children.Add(new HostsGuestRequestUC(i, host));
                }
            }
            catch (Exceptions.NoItemsException)
            {
            }
        }

        private void Refresh()
        {
            HostingUnitStack.Children.Clear();
            HostingUnitStack.Children.Add(Resources["TitlesBar"] as Grid);
            try
            {
                var li = from i in CreateAccount.myBL.getHostingUnitByHost()
                         where i.Key.HostKey == FR_Imp.GetFR().GetHostKey(LoginPage.UserName)
                         select new { Hostingunits = i };

                foreach (var i in li)
                {
                    foreach (var item in i.Hostingunits)
                    {
                        HostingUnitStack.Children.Add(new HostingUnitUC(this, item));
                    }
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Complete_Refresh();
        }
        internal void RemoveHostingUnit(int key)
        {
            System.Media.SystemSounds.Hand.Play();
            var dialogResult = MessageBox.Show("Are you sure you want to delete the hosting unit?\nNote! This will permanently delete the hosting unit and all related Orders!", "Alert!", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                CreateAccount.myBL.RemoveHostingUnit(key);
                Complete_Refresh();
            }
        }

        private void Complete_Refresh()
        {
            Refresh();
            LoadGuestRequests();
        }

        internal void LoadMatchesForHU(BE.HostingUnit hu)
        {
            Tabs.SelectedIndex = 1;
            GuestRequestStack.Children.Clear();
            (Resources["GuestTitlesBar"] as Grid).ToolTip = "Click refresh to load all guest requests";
            GuestRequestStack.Children.Add(Resources["GuestTitlesBar"] as Grid);

            List<BE.GuestRequest> list = new List<BE.GuestRequest>();


            list.AddRange(CreateAccount.myBL.GetMatchingGuestRequests(hu));

            foreach (var i in list)
            {
                var s = new HostsGuestRequestUC(i, host);
                s.ToolTip = "Click refresh to load all guest requests";
                GuestRequestStack.Children.Add(s);

            }
        }

        private void LogOffButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
