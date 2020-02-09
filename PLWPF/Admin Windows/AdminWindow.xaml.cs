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
        // TODO: Add sort option

        public AdminWindow()
        {
            InitializeComponent();
            Refresh_BankBranches();
            Complete_Refresh();
            UpDownControl.Value = GlobalVars.myBL.GetCommission();
        }

        #region Private Refreshing Functions

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
            var list = GlobalVars.myBL.GetAllGuests();
            foreach (var i in list)
            {
                GuestStack.Children.Add(new AdminGuestUC(i));
            }
        }

        private void Refresh_BankBranches()
        {
            BankBranchStack.Children.Clear();
            var list = GlobalVars.myBL.GetAllBankAccounts();
            foreach (var i in list)
            {
                BankBranchStack.Children.Add(new AdminBankBranchUC(i));
            }
        }

        private void Refresh_GuestComments()
        {
            GuestComments.Children.Clear();
            var comments = GlobalVars.myBL.GetAllGuestComments().ToList();
            comments.Sort();

            foreach (var comment in comments)
            {
                GuestComments.Children.Add(new Comment(comment));
                GuestComments.Children.Add(new Separator());
            }
        }

        private void Refresh_HostComments()
        {
            HostComments.Children.Clear();
            var comments = GlobalVars.myBL.GetAllHostComments().ToList();
            comments.Sort();

            foreach (var comment in comments)
            {
                HostComments.Children.Add(new Comment(comment));
                HostComments.Children.Add(new Separator());
            }
        }

        private void Refresh_UnitComments()
        {
            UnitComments.Children.Clear();
            var comments = GlobalVars.myBL.GetAllUnitComments().ToList();
            comments.Sort();

            foreach (var comment in comments)
            {
                UnitComments.Children.Add(new Comment(comment));
                UnitComments.Children.Add(new Separator());
            }
        }

        #endregion

        #region Refreshing Sort Handlers

        private void HostSortRefresh(object sender, SelectionChangedEventArgs e)
        {
            if (HostStack is StackPanel) // So as not to fire before initialized
            {
                Complete_Refresh();
            }

            switch (HostComboBox.SelectedIndex)
            {
                case 1:
                    SortHostByPrivateName();
                    break;
                case 2:
                    SortHostByFamilyName();
                    break;
                case 3:
                    SortHostByKey();
                    break;
                default:
                    break;
            }
        }

        private void HostingUnitSortRefresh(object sender, SelectionChangedEventArgs e)
        {
            if (HUStack is StackPanel)
            {
                Complete_Refresh();
            }

            switch (HostingUnitComboBox.SelectedIndex)
            {
                case 1:
                    SortHostingUnitByHostName();
                    break;
                case 2:
                    SortHostingUnitByHostingUnitName();
                    break;
                case 3:
                    SortHostingUnitByKey();
                    break;
                default:
                    break;
            }
        }

        private void GuestRequestSortRefresh(object sender, SelectionChangedEventArgs e)
        {
            if (GRStack is StackPanel)
            {
                Complete_Refresh();
            }

            switch (GuestRequestComboBox.SelectedIndex)
            {
                case 1:
                    SortGuestRequestByName();
                    break;
                case 2:
                    SortGuestRequestByKey();
                    break;
                case 3:
                    SortGuestRequestByRegistrationDate();
                    break;
                case 4:
                    SortGuestRequestByEntryDate();
                    break;
                case 5:
                    SortGuestRequestByReleaseDate();
                    break;
                default:
                    break;
            }
        }

        private void GuestSortRefresh(object sender, SelectionChangedEventArgs e)
        {
            if (GuestStack is StackPanel) // So as not to fire before initialized
            {
                Complete_Refresh();
            }

            switch (GuestComboBox.SelectedIndex)
            {
                case 1:
                    SortGuestByPrivateName();
                    break;
                case 2:
                    SortGuestByFamilyName();
                    break;
                case 3:
                    SortGuestByKey();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Refreshing By Sort Functions

        #region Host

        private void SortHostByPrivateName()
        {
            HostStack.Children.Clear();


            try
            {
                List<BE.Host> hosts = GlobalVars.myBL.GetAllHosts().ToList();
                hosts.Sort((x, t) => x.PrivateName.CompareTo(t.PrivateName));

                var li = from i in hosts
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

        private void SortHostByFamilyName()
        {
            HostStack.Children.Clear();

            try
            {
                List<BE.Host> hosts = GlobalVars.myBL.GetAllHosts().ToList();
                hosts.Sort((x, t) => x.FamilyName.CompareTo(t.FamilyName));

                var li = from i in hosts
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

        private void SortHostByKey()
        {
            HostStack.Children.Clear();

            try
            {
                List<BE.Host> hosts = GlobalVars.myBL.GetAllHosts().ToList();
                hosts.Sort((x, t) => x.HostKey.CompareTo(t.HostKey));

                var li = from i in hosts
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

        #endregion

        #region HostingUnit

        private void SortHostingUnitByHostName()
        {
            HUStack.Children.Clear();

            try
            {
                List<BE.HostingUnit> hostingUnits = GlobalVars.myBL.GetAllHostingUnits().ToList();
                hostingUnits.Sort((x, t) =>
                string.Format("{0} {1}", x.Owner.PrivateName, x.Owner.FamilyName)
                .CompareTo
                (string.Format("{0} {1}", t.Owner.PrivateName, t.Owner.FamilyName)));

                var li = from i in hostingUnits
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

        private void SortHostingUnitByHostingUnitName()
        {
            HUStack.Children.Clear();

            try
            {
                List<BE.HostingUnit> hostingUnits = GlobalVars.myBL.GetAllHostingUnits().ToList();
                hostingUnits.Sort((x, t) => x.HostingUnitName.CompareTo(t.HostingUnitName));

                var li = from i in hostingUnits
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

        private void SortHostingUnitByKey()
        {
            HUStack.Children.Clear();

            try
            {
                List<BE.HostingUnit> hostingUnits = GlobalVars.myBL.GetAllHostingUnits().ToList();
                hostingUnits.Sort((x, t) => x.HostingUnitKey.CompareTo(t.HostingUnitKey));

                var li = from i in hostingUnits
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

        #endregion

        #region GuestRequest

        private void SortGuestRequestByName()
        {
            GRStack.Children.Clear();

            try
            {
                var grs = GlobalVars.myBL.GetAllGuestRequests().ToList();
                grs.Sort((x, t) => string.Format("{0} {1}", x.Requester.PrivateName, x.Requester.FamilyName)
                .CompareTo(
                string.Format("{0} {1}", t.Requester.PrivateName, t.Requester.FamilyName)));

                var li = from i in grs
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

        private void SortGuestRequestByKey()
        {
            GRStack.Children.Clear();

            try
            {
                var grs = GlobalVars.myBL.GetAllGuestRequests().ToList();
                grs.Sort((x, t) => x.GuestRequestKey.CompareTo(t.GuestRequestKey));

                var li = from i in grs
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

        private void SortGuestRequestByRegistrationDate()
        {
            GRStack.Children.Clear();

            try
            {
                var grs = GlobalVars.myBL.GetAllGuestRequests().ToList();
                grs.Sort((x, t) => x.RegistrationDate.CompareTo(t.RegistrationDate));

                var li = from i in grs
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

        private void SortGuestRequestByEntryDate()
        {
            GRStack.Children.Clear();

            try
            {
                var grs = GlobalVars.myBL.GetAllGuestRequests().ToList();
                grs.Sort((x, t) => x.EntryDate.CompareTo(t.EntryDate));

                var li = from i in grs
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

        private void SortGuestRequestByReleaseDate()
        {
            GRStack.Children.Clear();

            try
            {
                var grs = GlobalVars.myBL.GetAllGuestRequests().ToList();
                grs.Sort((x, t) => x.ReleaseDate.CompareTo(t.ReleaseDate));

                var li = from i in grs
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


        #endregion

        #region Guest

        private void SortGuestByPrivateName()
        {
            GuestStack.Children.Clear();


            try
            {
                List<BE.Guest> guests = GlobalVars.myBL.GetAllGuests().ToList();
                guests.Sort((x, t) => x.PrivateName.CompareTo(t.PrivateName));

                var li = from i in guests
                         select i;

                foreach (var i in li)
                {
                    GuestStack.Children.Add(new AdminGuestUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void SortGuestByFamilyName()
        {
            GuestStack.Children.Clear();


            try
            {
                List<BE.Guest> guests = GlobalVars.myBL.GetAllGuests().ToList();
                guests.Sort((x, t) => x.FamilyName.CompareTo(t.FamilyName));

                var li = from i in guests
                         select i;

                foreach (var i in li)
                {
                    GuestStack.Children.Add(new AdminGuestUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        private void SortGuestByKey()
        {
            GuestStack.Children.Clear();


            try
            {
                List<BE.Guest> guests = GlobalVars.myBL.GetAllGuests().ToList();
                guests.Sort((x, t) => x.GuestKey.CompareTo(t.GuestKey));

                var li = from i in guests
                         select i;

                foreach (var i in li)
                {
                    GuestStack.Children.Add(new AdminGuestUC(i));
                }
            }
            catch (Exceptions.NoItemsException)
            {
                return;
            }
        }

        #endregion

        #endregion

        internal void Complete_Refresh()
        {
            Refresh_GuestRequests();
            Refresh_Hosts();
            Refresh_HostingUnits();
            Refresh_Orders();
            Refresh_Guests();
            Refresh_HostComments();
            Refresh_GuestComments();
            Refresh_UnitComments();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            Complete_Refresh();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpDownControl_LostFocus_1(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.SetCommission(UpDownControl.Value);

        }
    }
}
