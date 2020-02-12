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
using BE;

namespace PLWPF.Guest_Windows
{
    /// <summary>
    /// Interaction logic for OrderGUinfo.xaml
    /// </summary>
    public partial class OrderGUinfo : UserControl
    {
        private Order ord;
        private GuestRequest gr;
        public OrderGUinfo(Order ord, GuestRequest gr)
        {
            InitializeComponent();
            this.ord = ord;
            this.gr = gr;
            hostintUnitNameTextBlock.Text = GlobalVars.myBL.GetHostingUnit(ord.HostingUnitKey).HostingUnitName;
            keyTextBlock.Text = ord.OrderKey.ToString();
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.UpdateGuestRequestStatus(gr.GuestRequestKey, Enums.RequestStatus.ClosedWithDeal);
            GlobalVars.myBL.UpdateOrderStatus(ord.OrderKey, Enums.OrderStatus.ClosedByCustomerResponsiveness);
            MessageBox.Show("Application approved, enjoy your vacation");
        }

        private void DeclineButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalVars.myBL.UpdateOrderStatus(ord.OrderKey, Enums.OrderStatus.ClosedByCustomerUnresponsiveness);
            MessageBox.Show("Order rejection was successful");
        }
    }
}
