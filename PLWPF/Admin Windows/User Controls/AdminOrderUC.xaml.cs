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
    /// Interaction logic for AdminOrderUC.xaml
    /// </summary>
    public partial class AdminOrderUC : UserControl
    {
        private BE.Order ord;
        public AdminOrderUC(BE.Order ord)
        {
            InitializeComponent();
            HUKey.Content = ord.HostingUnitKey;
            GRKey.Content = ord.GuestRequestKey;
            Status.Content = ord.Status;

            this.ord = ord;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.UpdateOrderStatus(ord.OrderKey, BE.Enums.OrderStatus.ClosedByHost);
        }
    }
}
