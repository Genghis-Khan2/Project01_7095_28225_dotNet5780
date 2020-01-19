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
    /// Interaction logic for AdminHostingUnitUC.xaml
    /// </summary>
    public partial class AdminHostingUnitUC : UserControl
    {
        private BE.HostingUnit hu;
        public AdminHostingUnitUC(BE.HostingUnit hu)
        {
            InitializeComponent();
            Name.Content = hu.HostingUnitName;
            Owner.Content = hu.Owner.PrivateName + " " + hu.Owner.FamilyName;
            Key.Content = hu.HostingUnitKey;
            this.hu = hu;
        }


        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var window = new AdminHUInfoWindow(hu);
            window.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreateAccount.myBL.RemoveHostingUnit(hu.HostingUnitKey);
        }
    }
}
