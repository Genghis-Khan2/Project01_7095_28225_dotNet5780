using PLWPF.Admin_Windows;
using PLWPF.Admin_Windows.User_Controls;
using PLWPF.Host_Windows.User_Controls;
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
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        // TODO: Add remove option

        public AdminWindow()
        {
            InitializeComponent();
            Complete_Refresh();
        }

        private void Refresh_GuestRequests()
        {
            GRStack.Children.Clear();
            try
            {
                var li = from i in GlobalVars.myBL.GetAllGuestRequests()
                         select i;
                foreach (var i in li)
                {
                    GRStack.Children.Add(new AdminGuestRequestUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_Hosts()
        {
            HostStack.Children.Clear();
            try
            {
                var li = from i in GlobalVars.myBL.GetAllHosts()
                         select i;
                foreach (var i in li)
                {
                    HostStack.Children.Add(new AdminHostUC(this, i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_HostingUnits()
        {
            HUStack.Children.Clear();
            try
            {
                var li = from i in GlobalVars.myBL.GetAllHostingUnits()
                         select i;
                foreach (var i in li)
                {
                    HUStack.Children.Add(new AdminHostingUnitUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_Orders()
        {
            OrderStack.Children.Clear();

            try
            {
                var li = from i in GlobalVars.myBL.GetAllOrders()
                         select i;
                foreach (var i in li)
                {
                    OrderStack.Children.Add(new AdminOrderUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void Refresh_Guests()
        {
            GuestStack.Children.Clear();
            var list = FR.FR_Imp.GetFR().GetListOfGuestNames();
            foreach (var i in list)
            {
                GuestStack.Children.Add(new AdminGuestUC(i));
            }
        }

        internal void Complete_Refresh()
        {
            Refresh_GuestRequests();
            Refresh_Hosts();
            Refresh_HostingUnits();
            Refresh_Orders();
            Refresh_Guests();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Complete_Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
