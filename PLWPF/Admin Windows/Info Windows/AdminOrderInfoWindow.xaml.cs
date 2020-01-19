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
    /// Interaction logic for AdminOrderInfoWindow.xaml
    /// </summary>
    public partial class AdminOrderInfoWindow : Window
    {
        public AdminOrderInfoWindow(BE.Order ord)
        {
            InitializeComponent();
            LoadData(ord);
        }

        private void LoadData(BE.Order ord)
        {
            HUKey.Content = ord.HostingUnitKey;
            GRKey.Content = ord.GuestRequestKey;
            OrderKey.Content = ord.OrderKey;
            Status.Content = ord.Status;
            CreateDate.Content = string.Format("{0}.{1}", ord.CreateDate.Day, ord.CreateDate.Month);
            OrderDate.Content = string.Format("{0}.{1}", ord.OrderDate.Day, ord.OrderDate.Month);
        }
    }
}
